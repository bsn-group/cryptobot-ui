using System;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using CryptobotUi.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace CryptobotUi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _config;

        public EventsController(IConfiguration config)
        {
            _config = config;
        }

        // POST /api/events/raise
        [HttpPost]
        public Task<HttpResponseMessage> Raise(MarketEvent marketEvent)
        {
            var marketEventsUrl = _config.GetValue<string>("MarketEventsUrl");
            return client.PostAsync(marketEventsUrl, JsonContent.Create(marketEvent));
        }
    }
}
