using System;
using System.Threading.Tasks;
using CryptobotUi.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CryptobotUi
{
    [Route("api/[controller]")]
    public class DashboardController: ControllerBase
    {
        [HttpGet]
        public async Task<DashboardData> Get([FromQuery] DashboardFilter filter)
        {
            return new DashboardData {
                NetProfit = new Money(134, "USD"),
                NetProfitPercent = 101,
                AvgTradeDuration = TimeSpan.FromSeconds(10),
                AvgTradePnlPercent = 10,
                AvgTradeSize = new Money(2, "USD"),
                MaxDrawDown = new Money(3, "USD"),
                MaxDrawDownPercent = 4
            };
        }
    }
}