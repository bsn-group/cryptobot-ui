using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using CryptobotUi.Models.Cryptodb;
using CryptobotUi.Client.Model;

namespace CryptobotUi.Pages
{
    public partial class AddStrategyComponent
    {
        protected IEnumerable<LookupValue> positionTypes =>
            Enum.GetNames<PositionTypes>()
                .Select(p => new LookupValue { Name = p });

        protected IEnumerable<LookupValue> exchangeTypes =>
            Enum.GetNames<ExchangeTypes>()
                .Select(p => new LookupValue { Name = p });

        void SetName()
        {
            var template = $"{strategy.symbol}_{strategy.exchange_type}_{strategy.position_type}";
            strategy.name = template;
        }

        void AddCondition(StrategyCondition condition)
        {
            if (condition == null) return;
            try 
            {
                if (strategy.StrategyConditions is not List<StrategyCondition> conditions)
                {
                    conditions = 
                        strategy.StrategyConditions == null 
                        ? new List<StrategyCondition>() 
                        : strategy.StrategyConditions.ToList(); // make a copy

                    strategy.StrategyConditions = conditions; // set it back to the copied reference
                }

                conditions.Add(condition);
                
                conditionsGrid.Reload();
            } 
            catch (Exception ex) 
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to add new condition! {ex.Message}" });
                JSRuntime.Log($"AddCondition Error: {ex}");
            }
        }

        // Creating a custom method for creating strategy, because the 
        async Task CreateStrategy()
        {
            try
            {
                var cryptodbCreateStrategyResult = await Cryptodb.CreateStrategyWithConditions(strategy);
                DialogService.Close(strategy);
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to create new Strategy! {ex.Message}" });
                JSRuntime.Log($"CreateStrategy Error: {ex}");
            }
        } 
    }
}
