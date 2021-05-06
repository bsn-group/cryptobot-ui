using System;
using System.Linq;
using System.Threading.Tasks;
using CryptobotUi.Client.Model;
using CryptobotUi.Models.Cryptodb;
using Microsoft.AspNetCore.Components;

namespace CryptobotUi.Pages
{
    public partial class StrategiesComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

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
    }

}
