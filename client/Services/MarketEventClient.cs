using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using CryptobotUi.Models.Cryptodb;
using Microsoft.AspNetCore.Components;

namespace CryptobotUi
{
    public class MarketEventClient
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly Radzen.NotificationService _notificationService;

        private readonly NavigationManager _urlHelper;
        public MarketEventClient(Radzen.NotificationService notificationService, NavigationManager urlHelper)
        {
            _notificationService = notificationService;
            _urlHelper = urlHelper;
        }

        public async Task RaiseMarketEvent(CryptobotUi.Models.Shared.MarketEvent evt)
        {
            if (evt is null)
            {
                throw new System.ArgumentNullException(nameof(evt));
            }

            var url = $"{_urlHelper.BaseUri}api/events/raise";
            var response = await httpClient.PostAsync(url, JsonContent.Create(evt));
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _notificationService.Notify(
                    Radzen.NotificationSeverity.Error,
                    $"Could not raise market event. Server returned: ${response.StatusCode}",
                    detail: responseContent, duration: 10000);
            }
        }
    }
}