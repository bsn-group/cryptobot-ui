using System;

namespace CryptobotUi.Models.Shared
{
    public record Money (decimal Value, string Currency);

    public class DashboardFilter
    {
        public string Symbol { get; set; }
        public PositionTypes? PositionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class DashboardData
    {
        public Money NetProfit { get; set; }
        public decimal NetProfitPercent { get; set; }

        public int TotalClosedTrades { get; set; }
        public int TotalWinningTrades { get; set; }
        public int TotalLosingTrades { get; set; }

        public decimal PercentProfitable => TotalClosedTrades > 0 ? 100 * TotalWinningTrades / TotalClosedTrades : 0;
        public decimal ProfitFactor { get; set; }
        public Money MaxDrawDown { get; set; }
        public decimal MaxDrawDownPercent { get; set; }
        
        public Money MinTradeSize { get; set; }
        public Money MaxTradeSize { get; set; }
        public Money AvgTradeSize { get; set; }

        public decimal MinTradePnlPercent { get; set; }
        public decimal MaxTradePnlPercent { get; set; }
        public decimal AvgTradePnlPercent { get; set; }

        public TimeSpan MinTradeDuration { get; set; }
        public TimeSpan MaxTradeDuration { get; set; }
        public TimeSpan AvgTradeDuration { get; set; }
    }
}