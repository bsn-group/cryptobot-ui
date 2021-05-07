using System.ComponentModel.DataAnnotations;

namespace CryptobotUi.Models.Shared
{
    /// { 
    ///     "name": "{{strategy.order.action}}_{{strategy.order.alert_message}}_bsn_5m_BNB", 
    ///     "price": {{close}}, 
    ///     "symbol": "BNBUSD_PERP", 
    ///     "market": "USD", 
    ///     "contracts": 50,
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
        public string Source { get; set; }
        [Required]
        [Range(1, 10080)]
        public long TimeFrame { get; set; }  
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Exchange { get; set; }
        public decimal Contracts { get; set; }
    }
}