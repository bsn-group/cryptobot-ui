using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using CryptobotUi.Models.Cryptodb;
using CryptobotUi.Client.Pages;

namespace CryptobotUi.Pages
{
    public partial class StrategyConditionsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected CryptodbService Cryptodb { get; set; }
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.StrategyCondition> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<CryptobotUi.Models.Cryptodb.StrategyCondition> _getStrategyConditionsResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.StrategyCondition> getStrategyConditionsResult
        {
            get
            {
                return _getStrategyConditionsResult;
            }
            set
            {
                if (!object.Equals(_getStrategyConditionsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStrategyConditionsResult", NewValue = value, OldValue = _getStrategyConditionsResult };
                    _getStrategyConditionsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getStrategyConditionsCount;
        protected int getStrategyConditionsCount
        {
            get
            {
                return _getStrategyConditionsCount;
            }
            set
            {
                if (!object.Equals(_getStrategyConditionsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStrategyConditionsCount", NewValue = value, OldValue = _getStrategyConditionsCount };
                    _getStrategyConditionsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddStrategyCondition>("Add Strategy Condition", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Cryptodb.ExportStrategyConditionsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Strategy", Select = "category,created_time,name,condition_group,sequence,time_frame,id,Strategy.symbol,version,last_observed" }, $"Strategy Conditions");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Cryptodb.ExportStrategyConditionsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Strategy", Select = "category,created_time,name,condition_group,sequence,time_frame,id,Strategy.symbol,version,last_observed" }, $"Strategy Conditions");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var cryptodbGetStrategyConditionsResult = await Cryptodb.GetStrategyConditions(filter:$@"(contains(category,""{search}"") or contains(name,""{search}"") or contains(condition_group,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Strategy", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getStrategyConditionsResult = cryptodbGetStrategyConditionsResult.Value.AsODataEnumerable();

                getStrategyConditionsCount = cryptodbGetStrategyConditionsResult.Count;
            }
            catch (System.Exception cryptodbGetStrategyConditionsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load StrategyConditions" });
            }
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var cryptodbDeleteStrategyConditionResult = await Cryptodb.DeleteStrategyCondition(id:data.id);
                    if (cryptodbDeleteStrategyConditionResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception cryptodbDeleteStrategyConditionException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete StrategyCondition" });
            }
        }
    }
}
