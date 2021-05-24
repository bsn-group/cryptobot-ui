namespace Cryptobot.Funnel.Analysis.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Cryptobot.Funnel.Analysis
open Microsoft.AspNetCore.Http
open FSharp.Data

type TradingViewStrategyCSV = CsvProvider<"Trade #,Type,Signal,Date/Time,Price,Contracts,Profit $,Profit %,Cum. Profit $,Cum. Profit %,Run-up $,Run-up %,Drawdown $,Drawdown %
1,Entry Short,Entershort,2021-05-19 19:12,0.42779,5000,33.18,1.75,33.18,0.66,46.13,2.26,1.87,0.1", HasHeaders = true>

type TradingViewAlertsCSV = CsvProvider<"""Alert ID,Ticker,Name,Description,Time
206960060,"BINANCE:MATICUSDT, 1m","Matic war 1m","{ 
        ""name"": ""buy_short_MATIC_War_1m"", 
        ""price"": 1.584525, 
        ""contracts"": 500,
        ""symbol"": ""MATICUSDT"", 
        ""market"": ""USDT"", 
        ""category"": ""bsn_war_matic"",
        ""timeFrame"": 1, 
        ""exchange"": ""Binance"" 
    }",2021-05-20T02:44:05.000Z""", HasHeaders = true>

type AlertJSON = JsonProvider<"""{
    "name": "buy_short_MATIC_War_1m",
    "price": 1.584525,
    "contracts": 500,
    "symbol": "MATICUSDT",
    "market": "USDT",
    "category": "bsn_war_matic",
    "timeFrame": 1,
    "exchange": "Binance"
}""">

type Trade = TradingViewStrategyCSV.Row
type Alert = TradingViewAlertsCSV.Row
type AlertDetail = AlertJSON.Root


[<ApiController>]
[<Route("[controller]")>]
type FileUploadController (logger : ILogger<FileUploadController>) =
    inherit ControllerBase()


    [<HttpPost>]
    member x.Post(strategyFile: IFormFile, alertsFile: IFormFile): FileUpload = 
        // use strategyTradesCsvStream = strategyFile.OpenReadStream()
        // let tvStratTrades = TradingViewStrategyCSV.Load(strategyTradesCsvStream)
        // let tradeNumber = tvStratTrades.Rows |> Seq.head |> (fun row -> row.)
        // tvStratTrades.Rows |> Seq.head |> (fun row -> row.)

        use tvStratTradesStream = strategyFile.OpenReadStream()
        let tvStratTradeSymbol = 
            strategyFile.FileName.Split '_'
            |> Seq.tryHead
            |> Option.defaultValue "UNKNOWN"



        let tvStratTrades = TradingViewStrategyCSV.Load tvStratTradesStream
        let makeKey (trade: Trade) = 
            let getOrderSide s = 
                match s with
                | "Entershort" -> "SELL"
                | "Enterlong" -> "BUY"
                | "Exitshort" -> "BUY"
                | "Exitlong" -> "SELL"
                | _ -> "UNKNOWN"

            let getPositionType s = 
                match s with
                | "Entershort" -> "SHORT"
                | "Enterlong" -> "LONG"
                | "Exitshort" -> "SHORT"
                | "Exitlong" -> "LONG"
                | _ -> "UNKNOWN"

            sprintf "%s-%s-%s-%s" 
                tvStratTradeSymbol 
                "binance" 
                (getOrderSide trade.Signal) 
                (getPositionType trade.Signal)
            |> fun s -> s.ToLower()

        let strategyTrades = 
            tvStratTrades.Rows 
            |> Seq.map (fun t -> makeKey t, t)
            |> Seq.groupBy (fun (k,_) -> k)
            |> Seq.map (fun (k,rs) -> k, rs |> Seq.map snd)
            |> Seq.map (fun (k,rs) -> k, rs |> Seq.sortBy (fun r -> r.``Date/Time``))
            |> dict        

        use alertsCsvStream = alertsFile.OpenReadStream()
        let alerts = TradingViewAlertsCSV.Load alertsCsvStream 

        let getAlertKey (j: AlertDetail) = 
            let getOrderSide (s: string) =
                s.Split '_'
                |> Seq.tryHead
                |> Option.defaultValue "UNKNOWN" 

            let getPositionType (s: string) =
                s.Split '_'
                |> fun f -> if Seq.isEmpty f then Seq.empty else Seq.skip 1 f
                |> Seq.tryHead 
                |> Option.defaultValue "UNKNOWN"

            sprintf "%s-%s-%s-%s" 
                j.Symbol
                j.Exchange
                (getOrderSide j.Name)
                (getPositionType j.Name)
            |> fun (s: string) -> s.ToLower()
                
        let getMatchTrades ((a, j): Alert * AlertDetail) = 
            let found, tradesForKey = strategyTrades.TryGetValue (getAlertKey j)
            let matchingTrades = 
                if found then tradesForKey else Seq.empty
                |> Seq.filter (fun t -> 
                        let diff = t.``Date/Time`` - a.Time.DateTime
                        Math.Abs (diff.TotalSeconds) < 30.0
                    )
            a, j, matchingTrades



        let functionx = 
            Result.map (fun (a, j, t) -> (0, 0, 0, 0)
                
                )
            // match r with
            // | Result.Ok (a, j, t) -> Ok (0, 0, 0, 0)
            // | Error s -> Result.Error s

        // Result<Trade, string>
        let alertsList =     
            alerts.Rows  
            |> Seq.map  (fun a -> a, AlertJSON.Parse a.Description)
            |> Seq.map getMatchTrades
            |> Seq.map (fun (a, j, ts) ->
                    match ts |> Seq.toList with 
                    | [] -> Error "No Matching Trades for Alert"
                    | [t] -> Ok (a, j, t)
                    | t::rest -> Error (sprintf "More than %d match found" ((List.length rest)+1))
                )
            |> Seq.map functionx
        (*
        1 - Upload 2 CSV files (in memory) TV Trades vs TV Alerts
            - Parse CSV into records
            - Diff between datetime, price, and contracts for a symbol, exchange, order side and order type : long/short
        2 - Alerts with Matching trades and Matching trades with Alerts
        *)

        { Status = "Number of Trades: " + string(Seq.length strategyTrades) + " Number of Alerts: " + string(Seq.length alertsList) }
        