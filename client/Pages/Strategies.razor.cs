using System;
using System.Linq;
using System.Threading.Tasks;
using CryptobotUi.Client.Model;
using CryptobotUi.Client.Pages;
using CryptobotUi.Models.Cryptodb;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CryptobotUi.Pages
{
    public partial class StrategiesComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected MarketEventClient MarketEventClient { get; set; }

        public async Task CreateStrategyCondition(StrategyCondition strategyCondition, long strategy_id) {
            if (strategyCondition != null)
            {
                try 
                {
                    AppState.IsBusy = true;
                    strategyCondition.strategy_id = strategy_id;
                    await this.Cryptodb.CreateStrategyCondition(strategyCondition);
                    
                    var strategy = grid0.Data.FirstOrDefault(s => s.id == strategy_id);
                    if (strategy != null)
                    {
                        await grid0.Reload();
                        await grid0.ExpandRow(strategy);
                    }
                }
                catch (Exception ex)
                {
                    NotificationService.Notify(Radzen.NotificationSeverity.Error, $"Error creating strategy condition: {ex.Message}");
                }
                finally
                {
                    AppState.IsBusy = false;
                }
            }
        }

        protected async Task RaiseMarketEvent(CryptobotUi.Models.Cryptodb.StrategyCondition condition)
        {
            var evt = condition == null
            ? new Models.Shared.MarketEvent()
            : new Models.Shared.MarketEvent
            {
                Name = condition.name,
                Symbol = _master?.symbol,
                TimeFrame = condition.time_frame,
                Source = "Manual",
                Category = condition.category
            };

            CryptobotUi.Models.Shared.MarketEvent result = await DialogService.OpenAsync<RaiseMarketEvent>("Raise market event",
                parameters: new System.Collections.Generic.Dictionary<string, object> {
                    { "MarketEvent", evt }
                },
                options: new DialogOptions {
                    Width = $"{800}px"
                });

            if (result != null)
            {
                await MarketEventClient.RaiseMarketEvent(result);
            }
        }
    }
}
