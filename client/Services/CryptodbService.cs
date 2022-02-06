
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

        public async System.Threading.Tasks.Task ExportConfigsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/configs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/configs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportConfigsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/configs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/configs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetConfigs(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Config>> GetConfigs(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Configs");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConfigs(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Config>>(response);
        }
        partial void OnCreateConfig(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Config> CreateConfig(CryptobotUi.Models.Cryptodb.Config config = default(CryptobotUi.Models.Cryptodb.Config))
        {
            var uri = new Uri(baseUri, $"Configs");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(config), Encoding.UTF8, "application/json");

            OnCreateConfig(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Config>(response);
        }

        public async System.Threading.Tasks.Task ExportExchangesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchanges/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchanges/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportExchangesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchanges/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchanges/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetExchanges(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Exchange>> GetExchanges(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Exchanges");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchanges(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Exchange>>(response);
        }
        partial void OnCreateExchange(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Exchange> CreateExchange(CryptobotUi.Models.Cryptodb.Exchange exchange = default(CryptobotUi.Models.Cryptodb.Exchange))
        {
            var uri = new Uri(baseUri, $"Exchanges");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(exchange), Encoding.UTF8, "application/json");

            OnCreateExchange(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Exchange>(response);
        }

        public async System.Threading.Tasks.Task ExportExchangeOrdersToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchangeorders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchangeorders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportExchangeOrdersToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/exchangeorders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/exchangeorders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetExchangeOrders(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.ExchangeOrder>> GetExchangeOrders(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchangeOrders(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.ExchangeOrder>>(response);
        }
        partial void OnCreateExchangeOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.ExchangeOrder> CreateExchangeOrder(CryptobotUi.Models.Cryptodb.ExchangeOrder exchangeOrder = default(CryptobotUi.Models.Cryptodb.ExchangeOrder))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(exchangeOrder), Encoding.UTF8, "application/json");

            OnCreateExchangeOrder(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.ExchangeOrder>(response);
        }

        public async System.Threading.Tasks.Task ExportMarketEventsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/marketevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/marketevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMarketEventsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/marketevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/marketevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetMarketEvents(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.MarketEvent>> GetMarketEvents(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MarketEvents");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMarketEvents(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.MarketEvent>>(response);
        }
        partial void OnCreateMarketEvent(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.MarketEvent> CreateMarketEvent(CryptobotUi.Models.Cryptodb.MarketEvent marketEvent = default(CryptobotUi.Models.Cryptodb.MarketEvent))
        {
            var uri = new Uri(baseUri, $"MarketEvents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(marketEvent), Encoding.UTF8, "application/json");

            OnCreateMarketEvent(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.MarketEvent>(response);
        }

        public async System.Threading.Tasks.Task ExportPnlsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/pnls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/pnls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPnlsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/pnls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/pnls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetPnls(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Pnl>> GetPnls(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Pnls");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPnls(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Pnl>>(response);
        }

        public async System.Threading.Tasks.Task ExportPositionsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/positions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/positions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportPositionsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/positions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/positions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetPositions(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Position>> GetPositions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Positions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetPositions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Position>>(response);
        }

        public async System.Threading.Tasks.Task ExportSignalsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/signals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/signals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSignalsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/signals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/signals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSignals(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Signal>> GetSignals(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Signals");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSignals(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Signal>>(response);
        }
        partial void OnCreateSignal(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Signal> CreateSignal(CryptobotUi.Models.Cryptodb.Signal signal = default(CryptobotUi.Models.Cryptodb.Signal))
        {
            var uri = new Uri(baseUri, $"Signals");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(signal), Encoding.UTF8, "application/json");

            OnCreateSignal(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Signal>(response);
        }

        public async System.Threading.Tasks.Task ExportSignalCommandsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/signalcommands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/signalcommands/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSignalCommandsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/signalcommands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/signalcommands/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSignalCommands(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.SignalCommand>> GetSignalCommands(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SignalCommands");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSignalCommands(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.SignalCommand>>(response);
        }
        partial void OnCreateSignalCommand(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.SignalCommand> CreateSignalCommand(CryptobotUi.Models.Cryptodb.SignalCommand signalCommand = default(CryptobotUi.Models.Cryptodb.SignalCommand))
        {
            var uri = new Uri(baseUri, $"SignalCommands");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(signalCommand), Encoding.UTF8, "application/json");

            OnCreateSignalCommand(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.SignalCommand>(response);
        }

        public async System.Threading.Tasks.Task ExportStrategiesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategies/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStrategiesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategies/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetStrategies(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Strategy>> GetStrategies(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Strategies");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategies(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Strategy>>(response);
        }
        partial void OnCreateStrategy(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Strategy> CreateStrategy(CryptobotUi.Models.Cryptodb.Strategy strategy = default(CryptobotUi.Models.Cryptodb.Strategy))
        {
            var uri = new Uri(baseUri, $"Strategies");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(strategy), Encoding.UTF8, "application/json");

            OnCreateStrategy(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Strategy>(response);
        }

        public async System.Threading.Tasks.Task ExportStrategyConditionsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategyconditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategyconditions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStrategyConditionsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/strategyconditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/strategyconditions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetStrategyConditions(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.StrategyCondition>> GetStrategyConditions(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"StrategyConditions");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategyConditions(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.StrategyCondition>>(response);
        }
        partial void OnCreateStrategyCondition(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.StrategyCondition> CreateStrategyCondition(CryptobotUi.Models.Cryptodb.StrategyCondition strategyCondition = default(CryptobotUi.Models.Cryptodb.StrategyCondition))
        {
            var uri = new Uri(baseUri, $"StrategyConditions");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(strategyCondition), Encoding.UTF8, "application/json");

            OnCreateStrategyCondition(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.StrategyCondition>(response);
        }

        public async System.Threading.Tasks.Task ExportSymbolsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/symbols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/symbols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSymbolsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cryptodb/symbols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cryptodb/symbols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSymbols(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Symbol>> GetSymbols(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Symbols");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSymbols(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<CryptobotUi.Models.Cryptodb.Symbol>>(response);
        }
        partial void OnCreateSymbol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Symbol> CreateSymbol(CryptobotUi.Models.Cryptodb.Symbol symbol = default(CryptobotUi.Models.Cryptodb.Symbol))
        {
            var uri = new Uri(baseUri, $"Symbols");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(symbol), Encoding.UTF8, "application/json");

            OnCreateSymbol(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Symbol>(response);
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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Config> GetConfigByname(string expand = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"Configs('{HttpUtility.UrlEncode(name.Trim())}')");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetConfigByname(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Config>(response);
        }
        partial void OnUpdateConfig(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateConfig(string name = default(string), CryptobotUi.Models.Cryptodb.Config config = default(CryptobotUi.Models.Cryptodb.Config))
        {
            var uri = new Uri(baseUri, $"Configs('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", config.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(config), Encoding.UTF8, "application/json");

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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Exchange> GetExchangeByid(string expand = default(string), Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Exchanges({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchangeByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Exchange>(response);
        }
        partial void OnUpdateExchange(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateExchange(Int64? id = default(Int64?), CryptobotUi.Models.Cryptodb.Exchange exchange = default(CryptobotUi.Models.Cryptodb.Exchange))
        {
            var uri = new Uri(baseUri, $"Exchanges({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", exchange.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(exchange), Encoding.UTF8, "application/json");

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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.ExchangeOrder> GetExchangeOrderByid(string expand = default(string), Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetExchangeOrderByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.ExchangeOrder>(response);
        }
        partial void OnUpdateExchangeOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateExchangeOrder(Int64? id = default(Int64?), CryptobotUi.Models.Cryptodb.ExchangeOrder exchangeOrder = default(CryptobotUi.Models.Cryptodb.ExchangeOrder))
        {
            var uri = new Uri(baseUri, $"ExchangeOrders({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", exchangeOrder.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(exchangeOrder), Encoding.UTF8, "application/json");

            OnUpdateExchangeOrder(httpRequestMessage);

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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.MarketEvent> GetMarketEventByid(string expand = default(string), Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"MarketEvents({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMarketEventByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.MarketEvent>(response);
        }
        partial void OnUpdateMarketEvent(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateMarketEvent(Int64? id = default(Int64?), CryptobotUi.Models.Cryptodb.MarketEvent marketEvent = default(CryptobotUi.Models.Cryptodb.MarketEvent))
        {
            var uri = new Uri(baseUri, $"MarketEvents({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", marketEvent.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(marketEvent), Encoding.UTF8, "application/json");

            OnUpdateMarketEvent(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSignal(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSignal(Int64? signalId = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Signals({signalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSignal(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSignalBysignalId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Signal> GetSignalBysignalId(string expand = default(string), Int64? signalId = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Signals({signalId})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSignalBysignalId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Signal>(response);
        }
        partial void OnUpdateSignal(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSignal(Int64? signalId = default(Int64?), CryptobotUi.Models.Cryptodb.Signal signal = default(CryptobotUi.Models.Cryptodb.Signal))
        {
            var uri = new Uri(baseUri, $"Signals({signalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", signal.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(signal), Encoding.UTF8, "application/json");

            OnUpdateSignal(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSignalCommand(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSignalCommand(Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"SignalCommands({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSignalCommand(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSignalCommandByid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.SignalCommand> GetSignalCommandByid(string expand = default(string), Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"SignalCommands({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSignalCommandByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.SignalCommand>(response);
        }
        partial void OnUpdateSignalCommand(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSignalCommand(Int64? id = default(Int64?), CryptobotUi.Models.Cryptodb.SignalCommand signalCommand = default(CryptobotUi.Models.Cryptodb.SignalCommand))
        {
            var uri = new Uri(baseUri, $"SignalCommands({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", signalCommand.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(signalCommand), Encoding.UTF8, "application/json");

            OnUpdateSignalCommand(httpRequestMessage);

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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Strategy> GetStrategyByid(string expand = default(string), Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"Strategies({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategyByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Strategy>(response);
        }
        partial void OnUpdateStrategy(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateStrategy(Int64? id = default(Int64?), CryptobotUi.Models.Cryptodb.Strategy strategy = default(CryptobotUi.Models.Cryptodb.Strategy))
        {
            var uri = new Uri(baseUri, $"Strategies({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", strategy.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(strategy), Encoding.UTF8, "application/json");

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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.StrategyCondition> GetStrategyConditionByid(string expand = default(string), Int64? id = default(Int64?))
        {
            var uri = new Uri(baseUri, $"StrategyConditions({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStrategyConditionByid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.StrategyCondition>(response);
        }
        partial void OnUpdateStrategyCondition(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateStrategyCondition(Int64? id = default(Int64?), CryptobotUi.Models.Cryptodb.StrategyCondition strategyCondition = default(CryptobotUi.Models.Cryptodb.StrategyCondition))
        {
            var uri = new Uri(baseUri, $"StrategyConditions({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", strategyCondition.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(strategyCondition), Encoding.UTF8, "application/json");

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


        public async System.Threading.Tasks.Task<CryptobotUi.Models.Cryptodb.Symbol> GetSymbolByname(string expand = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"Symbols('{HttpUtility.UrlEncode(name.Trim())}')");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSymbolByname(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<CryptobotUi.Models.Cryptodb.Symbol>(response);
        }
        partial void OnUpdateSymbol(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSymbol(string name = default(string), CryptobotUi.Models.Cryptodb.Symbol symbol = default(CryptobotUi.Models.Cryptodb.Symbol))
        {
            var uri = new Uri(baseUri, $"Symbols('{HttpUtility.UrlEncode(name.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", symbol.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(symbol), Encoding.UTF8, "application/json");

            OnUpdateSymbol(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
