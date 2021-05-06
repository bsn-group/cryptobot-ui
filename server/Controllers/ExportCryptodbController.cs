using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptobotUi.Data;

namespace CryptobotUi
{
    public partial class ExportCryptodbController : ExportController
    {
        private readonly CryptodbContext context;

        public ExportCryptodbController(CryptodbContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/Cryptodb/configs/csv")]
        [HttpGet("/export/Cryptodb/configs/csv(fileName='{fileName}')")]
        public FileStreamResult ExportConfigsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Configs, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/configs/excel")]
        [HttpGet("/export/Cryptodb/configs/excel(fileName='{fileName}')")]
        public FileStreamResult ExportConfigsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Configs, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/exchanges/csv")]
        [HttpGet("/export/Cryptodb/exchanges/csv(fileName='{fileName}')")]
        public FileStreamResult ExportExchangesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Exchanges, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/exchanges/excel")]
        [HttpGet("/export/Cryptodb/exchanges/excel(fileName='{fileName}')")]
        public FileStreamResult ExportExchangesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Exchanges, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/exchangeorders/csv")]
        [HttpGet("/export/Cryptodb/exchangeorders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportExchangeOrdersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.ExchangeOrders, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/exchangeorders/excel")]
        [HttpGet("/export/Cryptodb/exchangeorders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportExchangeOrdersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.ExchangeOrders, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/futurespnls/csv")]
        [HttpGet("/export/Cryptodb/futurespnls/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesPnlsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.FuturesPnls, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/futurespnls/excel")]
        [HttpGet("/export/Cryptodb/futurespnls/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesPnlsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.FuturesPnls, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/futurespositions/csv")]
        [HttpGet("/export/Cryptodb/futurespositions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesPositionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.FuturesPositions, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/futurespositions/excel")]
        [HttpGet("/export/Cryptodb/futurespositions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesPositionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.FuturesPositions, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/futuressignals/csv")]
        [HttpGet("/export/Cryptodb/futuressignals/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesSignalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.FuturesSignals, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/futuressignals/excel")]
        [HttpGet("/export/Cryptodb/futuressignals/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesSignalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.FuturesSignals, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/futuressignalcommands/csv")]
        [HttpGet("/export/Cryptodb/futuressignalcommands/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesSignalCommandsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.FuturesSignalCommands, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/futuressignalcommands/excel")]
        [HttpGet("/export/Cryptodb/futuressignalcommands/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFuturesSignalCommandsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.FuturesSignalCommands, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/marketevents/csv")]
        [HttpGet("/export/Cryptodb/marketevents/csv(fileName='{fileName}')")]
        public FileStreamResult ExportMarketEventsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.MarketEvents, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/marketevents/excel")]
        [HttpGet("/export/Cryptodb/marketevents/excel(fileName='{fileName}')")]
        public FileStreamResult ExportMarketEventsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.MarketEvents, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/strategies/csv")]
        [HttpGet("/export/Cryptodb/strategies/csv(fileName='{fileName}')")]
        public FileStreamResult ExportStrategiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Strategies, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/strategies/excel")]
        [HttpGet("/export/Cryptodb/strategies/excel(fileName='{fileName}')")]
        public FileStreamResult ExportStrategiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Strategies, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/strategyconditions/csv")]
        [HttpGet("/export/Cryptodb/strategyconditions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportStrategyConditionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.StrategyConditions, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/strategyconditions/excel")]
        [HttpGet("/export/Cryptodb/strategyconditions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportStrategyConditionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.StrategyConditions, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/symbols/csv")]
        [HttpGet("/export/Cryptodb/symbols/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSymbolsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Symbols, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/symbols/excel")]
        [HttpGet("/export/Cryptodb/symbols/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSymbolsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Symbols, Request.Query), fileName);
        }
    }
}
