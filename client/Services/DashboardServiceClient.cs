using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using CryptobotUi.Models.Cryptodb;
using CryptobotUi.Models.Shared;
using Microsoft.AspNetCore.Components;

namespace CryptobotUi
{
    public class DashboardServiceClient
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly Radzen.NotificationService _notificationService;

        private readonly NavigationManager _urlHelper;
        public DashboardServiceClient(Radzen.NotificationService notificationService, NavigationManager urlHelper)
        {
            _notificationService = notificationService;
            _urlHelper = urlHelper;
        }

        public async Task<DashboardData> GetDashboardData(DashboardFilter filter)
        {
            var url = $"{_urlHelper.BaseUri}api/dashboard";
            var response = await httpClient.GetAsync(BuildUrlWithFilterQuery(url, filter));
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _notificationService.Notify(
                    Radzen.NotificationSeverity.Error,
                    $"Could not raise market event. Server returned: ${response.StatusCode}",
                    detail: responseContent, duration: 10000);
                
                return default;
            }

            return await response.Content.ReadFromJsonAsync<DashboardData>();
        }

        private Uri BuildUrlWithFilterQuery(string url, DashboardFilter filter)
        {
            var builder = new StringBuilder();
            
            if (filter.StartDate != DateTime.MinValue)
            {
                builder.Append("startDate=").Append(filter.StartDate);
            }

            if (filter.EndDate != DateTime.MinValue)
            {
                builder.Append("endDate=").Append(filter.EndDate);
            }

            if (filter.PositionType != null)
            {
                builder.Append("positionType=").Append(filter.PositionType);
            }

            if (!string.IsNullOrWhiteSpace(filter.Symbol))
            {
                builder.Append("symbol=").Append(filter.Symbol);
            }

            var urlBuilder = new UriBuilder(url);
            urlBuilder.Query = builder.Length > 0 ? "?" + builder.ToString() : urlBuilder.Query;
            return urlBuilder.Uri;
        }
    }
}