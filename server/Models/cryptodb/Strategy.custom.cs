using System.Collections.Generic;

namespace CryptobotUi.Models.Cryptodb
{
    partial class Strategy
    {
        public Strategy()
        {
            this.position_type = PositionTypes.LONG.ToString();
            this.exchange_type = ExchangeTypes.FUTURES.ToString();
            this.created_time = System.DateTime.UtcNow;
            this.updated_time = System.DateTime.UtcNow;
            this.StrategyConditions = new List<StrategyCondition>();
            this.status = StrategyStatus.ACTIVE.ToString();
        }
    }
}