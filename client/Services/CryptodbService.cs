
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Radzen;
using CryptobotUi.Models.Cryptodb;

namespace CryptobotUi
{
    public partial class CryptodbService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public CryptodbService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/cryptodb/");
        }

        public async System.Threading.Tasks.Task ExportConfigsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/configs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/configs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportConfigsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/configs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/configs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetConfigs(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Config>> GetConfigs(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Configs");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConfigs(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Config>>();
        }
        partial void OnCreateConfig(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Config> CreateConfig(Config config = default(Config))
        {
            var uri = new Uri(baseUri, $"Configs");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(config), Encoding.UTF8, "application/json");

            OnCreateConfig(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Config>();
        }

        public async System.Threading.Tasks.Task ExportExchangesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchanges/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchanges/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportExchangesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchanges/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchanges/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetExchanges(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Exchange>> GetExchanges(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Exchanges");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchanges(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Exchange>>();
        }
        partial void OnCreateExchange(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Exchange> CreateExchange(Exchange exchange = default(Exchange))
        {
            var uri = new Uri(baseUri, $"Exchanges");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(exchange), Encoding.UTF8, "application/json");

            OnCreateExchange(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Exchange>();
        }

        public async System.Threading.Tasks.Task ExportExchangeOrdersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchangeorders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchangeorders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportExchangeOrdersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchangeorders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchangeorders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetExchangeOrders(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<ExchangeOrder>> GetExchangeOrders(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchangeOrders(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<ExchangeOrder>>();
        }
        partial void OnCreateExchangeOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ExchangeOrder> CreateExchangeOrder(ExchangeOrder exchangeOrder = default(ExchangeOrder))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(exchangeOrder), Encoding.UTF8, "application/json");

            OnCreateExchangeOrder(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ExchangeOrder>();
        }

        public async System.Threading.Tasks.Task ExportFuturesPnlsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futurespnls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futurespnls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFuturesPnlsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futurespnls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futurespnls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetFuturesPnls(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<FuturesPnl>> GetFuturesPnls(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"FuturesPnls");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFuturesPnls(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<FuturesPnl>>();
        }

        public async System.Threading.Tasks.Task ExportFuturesPositionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futurespositions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futurespositions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFuturesPositionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futurespositions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futurespositions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetFuturesPositions(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<FuturesPosition>> GetFuturesPositions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"FuturesPositions");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFuturesPositions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<FuturesPosition>>();
        }

        public async System.Threading.Tasks.Task ExportFuturesSignalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futuressignals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futuressignals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFuturesSignalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futuressignals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futuressignals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetFuturesSignals(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<FuturesSignal>> GetFuturesSignals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"FuturesSignals");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFuturesSignals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<FuturesSignal>>();
        }
        partial void OnCreateFuturesSignal(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<FuturesSignal> CreateFuturesSignal(FuturesSignal futuresSignal = default(FuturesSignal))
        {
            var uri = new Uri(baseUri, $"FuturesSignals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(futuresSignal), Encoding.UTF8, "application/json");

            OnCreateFuturesSignal(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<FuturesSignal>();
        }

        public async System.Threading.Tasks.Task ExportFuturesSignalCommandsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futuressignalcommands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futuressignalcommands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFuturesSignalCommandsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/futuressignalcommands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/futuressignalcommands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetFuturesSignalCommands(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<FuturesSignalCommand>> GetFuturesSignalCommands(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"FuturesSignalCommands");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFuturesSignalCommands(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<FuturesSignalCommand>>();
        }
        partial void OnCreateFuturesSignalCommand(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<FuturesSignalCommand> CreateFuturesSignalCommand(FuturesSignalCommand futuresSignalCommand = default(FuturesSignalCommand))
        {
            var uri = new Uri(baseUri, $"FuturesSignalCommands");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(futuresSignalCommand), Encoding.UTF8, "application/json");

            OnCreateFuturesSignalCommand(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<FuturesSignalCommand>();
        }

        public async System.Threading.Tasks.Task ExportMarketEventsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/marketevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/marketevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMarketEventsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/marketevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/marketevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetMarketEvents(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<MarketEvent>> GetMarketEvents(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"MarketEvents");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMarketEvents(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<MarketEvent>>();
        }
        partial void OnCreateMarketEvent(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<MarketEvent> CreateMarketEvent(MarketEvent marketEvent = default(MarketEvent))
        {
            var uri = new Uri(baseUri, $"MarketEvents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(marketEvent), Encoding.UTF8, "application/json");

            OnCreateMarketEvent(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<MarketEvent>();
        }

        public async System.Threading.Tasks.Task ExportStrategiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStrategiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetStrategies(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Strategy>> GetStrategies(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Strategies");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategies(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Strategy>>();
        }
        partial void OnCreateStrategy(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Strategy> CreateStrategy(Strategy strategy = default(Strategy))
        {
            var uri = new Uri(baseUri, $"Strategies");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(strategy), Encoding.UTF8, "application/json");

            OnCreateStrategy(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Strategy>();
        }

        public async System.Threading.Tasks.Task ExportStrategyConditionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategyconditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategyconditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStrategyConditionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategyconditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategyconditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetStrategyConditions(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<StrategyCondition>> GetStrategyConditions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"StrategyConditions");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategyConditions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<StrategyCondition>>();
        }
        partial void OnCreateStrategyCondition(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<StrategyCondition> CreateStrategyCondition(StrategyCondition strategyCondition = default(StrategyCondition))
        {
            var uri = new Uri(baseUri, $"StrategyConditions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(strategyCondition), Encoding.UTF8, "application/json");

            OnCreateStrategyCondition(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<StrategyCondition>();
        }

        public async System.Threading.Tasks.Task ExportSymbolsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/symbols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/symbols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSymbolsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/symbols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/symbols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSymbols(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Symbol>> GetSymbols(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Symbols");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSymbols(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Symbol>>();
        }
        partial void OnCreateSymbol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Symbol> CreateSymbol(Symbol symbol = default(Symbol))
        {
            var uri = new Uri(baseUri, $"Symbols");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(symbol), Encoding.UTF8, "application/json");

            OnCreateSymbol(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Symbol>();
        }
        partial void OnDeleteConfig(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteConfig(string name = default(string))
        {
            var uri = new Uri(baseUri, $"Configs('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteConfig(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetConfigByname(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Config> GetConfigByname(string name = default(string))
        {
            var uri = new Uri(baseUri, $"Configs('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConfigByname(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Config>();
        }
        partial void OnUpdateConfig(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateConfig(string name = default(string), Config config = default(Config))
        {
            var uri = new Uri(baseUri, $"Configs('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", config.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(config), Encoding.UTF8, "application/json");

            OnUpdateConfig(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteExchange(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteExchange(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Exchanges({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteExchange(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetExchangeByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Exchange> GetExchangeByid(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Exchanges({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchangeByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Exchange>();
        }
        partial void OnUpdateExchange(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateExchange(Int64? id = default(Int64?), Exchange exchange = default(Exchange))
        {
            var uri = new Uri(baseUri, $"Exchanges({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", exchange.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(exchange), Encoding.UTF8, "application/json");

            OnUpdateExchange(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteExchangeOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteExchangeOrder(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteExchangeOrder(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetExchangeOrderByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ExchangeOrder> GetExchangeOrderByid(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchangeOrderByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ExchangeOrder>();
        }
        partial void OnUpdateExchangeOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateExchangeOrder(Int64? id = default(Int64?), ExchangeOrder exchangeOrder = default(ExchangeOrder))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", exchangeOrder.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(exchangeOrder), Encoding.UTF8, "application/json");

            OnUpdateExchangeOrder(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteFuturesSignal(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteFuturesSignal(Int64? signalId = default(Int64?))
        {
            var uri = new Uri(baseUri, $"FuturesSignals({signalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFuturesSignal(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetFuturesSignalBysignalId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<FuturesSignal> GetFuturesSignalBysignalId(Int64? signalId = default(Int64?))
        {
            var uri = new Uri(baseUri, $"FuturesSignals({signalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFuturesSignalBysignalId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<FuturesSignal>();
        }
        partial void OnUpdateFuturesSignal(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateFuturesSignal(Int64? signalId = default(Int64?), FuturesSignal futuresSignal = default(FuturesSignal))
        {
            var uri = new Uri(baseUri, $"FuturesSignals({signalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", futuresSignal.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(futuresSignal), Encoding.UTF8, "application/json");

            OnUpdateFuturesSignal(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteFuturesSignalCommand(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteFuturesSignalCommand(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"FuturesSignalCommands({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFuturesSignalCommand(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetFuturesSignalCommandByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<FuturesSignalCommand> GetFuturesSignalCommandByid(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"FuturesSignalCommands({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFuturesSignalCommandByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<FuturesSignalCommand>();
        }
        partial void OnUpdateFuturesSignalCommand(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateFuturesSignalCommand(Int64? id = default(Int64?), FuturesSignalCommand futuresSignalCommand = default(FuturesSignalCommand))
        {
            var uri = new Uri(baseUri, $"FuturesSignalCommands({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", futuresSignalCommand.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(futuresSignalCommand), Encoding.UTF8, "application/json");

            OnUpdateFuturesSignalCommand(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteMarketEvent(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteMarketEvent(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"MarketEvents({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMarketEvent(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetMarketEventByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<MarketEvent> GetMarketEventByid(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"MarketEvents({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMarketEventByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<MarketEvent>();
        }
        partial void OnUpdateMarketEvent(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateMarketEvent(Int64? id = default(Int64?), MarketEvent marketEvent = default(MarketEvent))
        {
            var uri = new Uri(baseUri, $"MarketEvents({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", marketEvent.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(marketEvent), Encoding.UTF8, "application/json");

            OnUpdateMarketEvent(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteStrategy(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteStrategy(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Strategies({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteStrategy(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetStrategyByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Strategy> GetStrategyByid(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Strategies({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategyByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Strategy>();
        }
        partial void OnUpdateStrategy(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateStrategy(Int64? id = default(Int64?), Strategy strategy = default(Strategy))
        {
            var uri = new Uri(baseUri, $"Strategies({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", strategy.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(strategy), Encoding.UTF8, "application/json");

            OnUpdateStrategy(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteStrategyCondition(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteStrategyCondition(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"StrategyConditions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteStrategyCondition(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetStrategyConditionByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<StrategyCondition> GetStrategyConditionByid(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"StrategyConditions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategyConditionByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<StrategyCondition>();
        }
        partial void OnUpdateStrategyCondition(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateStrategyCondition(Int64? id = default(Int64?), StrategyCondition strategyCondition = default(StrategyCondition))
        {
            var uri = new Uri(baseUri, $"StrategyConditions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", strategyCondition.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(strategyCondition), Encoding.UTF8, "application/json");

            OnUpdateStrategyCondition(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSymbol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSymbol(string name = default(string))
        {
            var uri = new Uri(baseUri, $"Symbols('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSymbol(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSymbolByname(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Symbol> GetSymbolByname(string name = default(string))
        {
            var uri = new Uri(baseUri, $"Symbols('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSymbolByname(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Symbol>();
        }
        partial void OnUpdateSymbol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSymbol(string name = default(string), Symbol symbol = default(Symbol))
        {
            var uri = new Uri(baseUri, $"Symbols('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", symbol.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(symbol), Encoding.UTF8, "application/json");

            OnUpdateSymbol(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
