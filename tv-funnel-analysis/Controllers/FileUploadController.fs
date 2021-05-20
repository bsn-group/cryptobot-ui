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
        let tvStratTrades = TradingViewStrategyCSV.Load tvStratTradesStream
        let strategyTrades = tvStratTrades.Rows |> Seq.toList

        use alertsCsvStream = alertsFile.OpenReadStream()
        let alerts = TradingViewAlertsCSV.Load alertsCsvStream 
        let alertsList = alerts.Rows  |> Seq.toList |> List.map (fun a -> a, AlertJSON.Parse a.Description)
        
        { Status = "Number of Trades: " + string strategyTrades.Length + " Number of Alerts: " + string alertsList.Length }
        