using System.ComponentModel.DataAnnotations;

namespace CryptobotUi.Client.Model
{
    /// { 
    ///     "name": "{{strategy.order.action}}_{{strategy.order.alert_message}}_bsn_5m_BNB", 
    ///     "price": {{close}}, 
    ///     "symbol": "BNBUSD_PERP", 
    ///     "market": "USD", 
    ///     "category": "bsn_occ_short",
    ///     "timeFrame": 5, 
    ///     "exchange": "Binance" 
    /// }
    public class MarketEvent
    {
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [Range(0.00000001, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Symbol { get; set; }
        public string Market { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Category { get; set; }
        [Required]
        [Range(5, 2440)]
        public int TimeFrame { get; set; }  

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Exchange { get; set; }
        public decimal Contracts { get; set; }
    }
}