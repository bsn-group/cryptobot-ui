using System.Linq;

namespace CryptobotUi.Controllers.Cryptodb
{
    partial class StrategiesController
    {
        partial void OnStrategyCreated(Models.Cryptodb.Strategy strategy)
        {
            // fix up strategy in the conditions, if any - so the ORM can save them all in one go.
            if (strategy.StrategyConditions != null)
            {
                foreach (var condition in strategy.StrategyConditions)
                {
                    condition.Strategy ??= strategy;
                }
            }
        }
    }
}
