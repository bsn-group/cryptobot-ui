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
        [HttpGet("/export/Cryptodb/pnls/csv")]
        [HttpGet("/export/Cryptodb/pnls/csv(fileName='{fileName}')")]
        public FileStreamResult ExportPnlsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Pnls, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/pnls/excel")]
        [HttpGet("/export/Cryptodb/pnls/excel(fileName='{fileName}')")]
        public FileStreamResult ExportPnlsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Pnls, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/positions/csv")]
        [HttpGet("/export/Cryptodb/positions/csv(fileName='{fileName}')")]
        public FileStreamResult ExportPositionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Positions, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/positions/excel")]
        [HttpGet("/export/Cryptodb/positions/excel(fileName='{fileName}')")]
        public FileStreamResult ExportPositionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Positions, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/signals/csv")]
        [HttpGet("/export/Cryptodb/signals/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSignalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Signals, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/signals/excel")]
        [HttpGet("/export/Cryptodb/signals/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSignalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Signals, Request.Query), fileName);
        }
        [HttpGet("/export/Cryptodb/signalcommands/csv")]
        [HttpGet("/export/Cryptodb/signalcommands/csv(fileName='{fileName}')")]
        public FileStreamResult ExportSignalCommandsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.SignalCommands, Request.Query), fileName);
        }

        [HttpGet("/export/Cryptodb/signalcommands/excel")]
        [HttpGet("/export/Cryptodb/signalcommands/excel(fileName='{fileName}')")]
        public FileStreamResult ExportSignalCommandsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.SignalCommands, Request.Query), fileName);
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
