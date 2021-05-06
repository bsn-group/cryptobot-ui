using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CryptobotUi.Models.Cryptodb;
using Radzen;

namespace CryptobotUi
{
    partial class CryptodbService
    {
        public async System.Threading.Tasks.Task<Strategy> CreateStrategyWithConditions(Strategy strategy)
        {
            var uri = new Uri(baseUri, $"Strategies");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(strategy), Encoding.UTF8, "application/json");

            OnCreateStrategy(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Strategy>();
        }
    
    }
}