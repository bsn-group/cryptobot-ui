using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using CryptobotUi.Client.Model;
using CryptobotUi.Models.Shared;

namespace CryptobotUi.Pages
{
    public partial class DashboardComponent
    {

        [Inject]
        protected AppState AppState { get; set; }

        [Inject]
        protected DashboardServiceClient DashboardServiceClient { get; set; }

        protected DashboardFilter Filter { get; set; } = new DashboardFilter();

        protected DashboardData Data { get; set; } = new DashboardData();

        protected string[] PositionTypes => Enum.GetNames<PositionTypes>();

        protected async Task LoadDashboard()
        {
            try 
            {
                AppState.IsBusy = true;
                Data = await DashboardServiceClient.GetDashboardData(Filter);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(
                    new NotificationMessage { 
                        Severity = NotificationSeverity.Error, 
                        Summary = $"Error", 
                        Detail = $"Unable to load dashboard: {ex.Message}"
                    });
                JSRuntime.Log($"Error: {ex}");
            }
            finally 
            {
                AppState.IsBusy = false;
                Reload();
            }
        }
    }
}
