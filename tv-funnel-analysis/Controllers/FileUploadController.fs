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
open Newtonsoft.Json

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
type AlertTradeDiff = {
    Error: string
    TVAlertName: string
    AlertId: int32
    Symbol: string
    AlertName: string
    Market: string 
    Category: string
    TimeFrame: int32
    Exchange: string
    AlertPrice: decimal
    AlertContracts: int32
    AlertDateTime: DateTime
    TradeId: int
    OrderSide: string
    PositionType: string
    TradeTime: DateTime
    TradeContracts: int32
    TradePrice: decimal
    PriceDiffPercent: decimal
    ContractsDiff: int32
    TimeDiff: float
    CompareType: string
}

[<ApiController>]
[<Route("[controller]")>]
type FileUploadController (logger : ILogger<FileUploadController>) =
    inherit ControllerBase()


    [<HttpPost>]
    member x.Post(strategyFile: IFormFile, alertsFile: IFormFile) = 

        use tvStratTradesStream = strategyFile.OpenReadStream()
        let tvStratTradeSymbol = 
            strategyFile.FileName.Split '_'
            |> Seq.tryHead
            |> Option.defaultValue "UNKNOWN"

        let getOrderSideForTrade s = 
            match s with
            | "Entry Short" -> "SELL"
            | "Exit Short" -> "BUY"
            | "Entry Long" -> "BUY"
            | "Exit Long" -> "SELL"
            | _ -> "UNKNOWN"

        let getPositionTypeForTrade s = 
            match s with
            | "Entry Short" -> "SHORT"
            | "Exit Short" -> "SHORT"
            | "Entry Long" -> "LONG"
            | "Exit Long" -> "LONG"
            | _ -> "UNKNOWN"


        let tvStratTrades = TradingViewStrategyCSV.Load tvStratTradesStream
        let tradeKey (trade: Trade) =            
            sprintf "%s-%s-%s-%s" 
                tvStratTradeSymbol 
                "binance" 
                (getOrderSideForTrade trade.Type) 
                (getPositionTypeForTrade trade.Type)
            |> fun s -> s.ToLower()

        let strategyTrades = 
            tvStratTrades.Rows 
            |> Seq.groupBy tradeKey
            |> Seq.map (fun (k,rs) -> k, rs |> Seq.sortBy (fun r -> r.``Date/Time``))
            |> dict        

        use alertsCsvStream = alertsFile.OpenReadStream()
        let alerts = TradingViewAlertsCSV.Load alertsCsvStream 

        let alertKey (j: AlertDetail) = 
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
                
        let getMatchingTrades ((a, j): Alert * AlertDetail) = 
            let found, tradesForKey = strategyTrades.TryGetValue (alertKey j)
            printfn "alertKey %s - %b" (alertKey j) found
            let matchingTrades = 
                if found then tradesForKey else Seq.empty
                |> Seq.filter (fun t -> 
                        let diff = t.``Date/Time`` - a.Time.LocalDateTime
                        Math.Abs (diff.TotalSeconds) < 30.0
                    )
            a, j, matchingTrades

        let findDiffs compareType (r: Result<Alert * AlertDetail * Trade, string>) = 
            match r with 
            | Ok (a, j, t) -> 
                { 
                    Error           = ""
                    TVAlertName     = a.Name
                    AlertId         = a.``Alert ID``
                    AlertName       = j.Name
                    Symbol          = j.Symbol
                    Market          = j.Market
                    Category        = j.Category
                    TimeFrame       = j.TimeFrame
                    Exchange        = j.Exchange
                    AlertPrice           = j.Price
                    AlertContracts  = j.Contracts
                    AlertDateTime   = a.Time.LocalDateTime
                    TradeId         = t.``Trade #``
                    OrderSide       = (getOrderSideForTrade t.Type) 
                    PositionType    = (getPositionTypeForTrade t.Type) 
                    TradeTime       = t.``Date/Time``
                    TradeContracts  = t.Contracts
                    TradePrice      = t.Price
                    PriceDiffPercent= (t.Price - j.Price) * 100M / j.Price
                    ContractsDiff   = (t.Contracts -  j.Contracts)
                    TimeDiff        = (t.``Date/Time`` - a.Time.LocalDateTime).TotalSeconds
                    CompareType     = compareType
                }  
            | Error s -> 
                { 
                    Error           = s
                    TVAlertName     = ""
                    AlertId         = 0
                    AlertName       = ""
                    Symbol          = ""
                    Market          = ""
                    Category        = ""
                    TimeFrame       = 0
                    Exchange        = ""
                    AlertPrice           = 0.0M
                    AlertContracts  = 0
                    AlertDateTime   = DateTime.MinValue
                    TradeId         = 0
                    OrderSide       = ""
                    PositionType    = ""
                    TradeTime       = DateTime.MinValue
                    TradeContracts  = 0
                    TradePrice      = 0.0M
                    PriceDiffPercent= 0.0M
                    ContractsDiff   = 0
                    TimeDiff        = 0.0
                    CompareType     = compareType
                    
                }  
            

        let alertsLookup =     
            alerts.Rows  
            |> Seq.map  (fun a -> a, AlertJSON.Parse a.Description)
            |> Seq.groupBy (fun (_, j) -> alertKey j)
            |> Seq.map (fun (k, v) -> k, Seq.sortBy (fun ((a: Alert), _) -> a.Time) v)
            |> dict

        let alertsDiffList =     
            alerts.Rows  
            |> Seq.map  (fun a -> a, AlertJSON.Parse a.Description)
            |> Seq.map getMatchingTrades
            |> Seq.map (fun (a, j, ts) ->
                    match ts |> Seq.toList with 
                    | [] -> Error "No Matching Trades found for Alert"
                    | [t] -> Ok (a, j, t)
                    | t::rest -> Error (sprintf "More than %d match found" ((List.length rest)+1))
                )
            |> Seq.map (fun r -> (findDiffs  "AlertWithTrades" r))
        (*
        1 - Upload 2 CSV files (in memory) TV Trades vs TV Alerts
            - Parse CSV into records
            - Diff between datetime, price, and contracts for a symbol, exchange, order side and order type : long/short
        2 - Alerts with Matching trades and Matching trades with Alerts
        *)
        let getMatchingAlerts t = 
            let found, alertsForKey = alertsLookup.TryGetValue (tradeKey t) 
            let matchingAlets = 
                if found then alertsForKey else Seq.empty
                |> Seq.filter (fun ((a: Alert), j) -> 
                        let diff = a.Time.LocalDateTime - t.``Date/Time`` // a.Time is in UTC timezone
                        printfn "sec diff %f " diff.TotalSeconds 
                        let df (date:DateTime) = date.ToString("yyyy.MMM.dd HH:mm:ss tt") + " " + date.Kind.ToString()
                        printfn "dates alert date %s - trade date %s" (df a.Time.LocalDateTime) (df t.``Date/Time``)
                        Math.Abs (diff.TotalSeconds) < 30.0
                    )
            t, matchingAlets

        let strategyTradesDiffList =
            tvStratTrades.Rows 
            |> Seq.map getMatchingAlerts 
            |> Seq.map (fun (t, alerts) -> 
                match alerts |> Seq.toList with
                | [] -> Error "No Matching Alerts found for Trade"
                | [aj] -> 
                    let (a, j) = aj
                    Ok (a, j, t)
                | a::rest -> Error (sprintf "More than %d match found" ((List.length rest)+1))
                )
            |> Seq.map (fun r -> (findDiffs  "TradeWithAlerts" r))

        Seq.append strategyTradesDiffList alertsDiffList
        