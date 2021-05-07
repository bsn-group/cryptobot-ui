using System;
using System.Linq;
using System.Collections.Generic;
using CryptobotUi.Client.Model;

namespace CryptobotUi.Pages
{
    public partial class AddStrategyConditionComponent
    {
        protected IEnumerable<LookupValue> conditionGroups =>
            Enum.GetNames<ConditionGroups>()
                .Select(p => new LookupValue { Name = p });

        protected void LoadStrategyDataFromMarketEvent(CryptobotUi.Models.Shared.MarketEvent marketEvent)
        {
            if (marketEvent != null)
            {
                var condition = this.strategycondition;
                condition.category = marketEvent.Category;
                condition.name = marketEvent.Name;
                condition.time_frame = marketEvent.TimeFrame;
                condition.last_observed = (long)(marketEvent.TimeFrame * 1.2);
            }
        }
    }
}
