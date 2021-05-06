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
    public partial class StrategiesComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.Strategy> grid0;
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.StrategyCondition> grid1;

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

        IEnumerable<CryptobotUi.Models.Cryptodb.Strategy> _getStrategiesResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.Strategy> getStrategiesResult
        {
            get
            {
                return _getStrategiesResult;
            }
            set
            {
                if (!object.Equals(_getStrategiesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStrategiesResult", NewValue = value, OldValue = _getStrategiesResult };
                    _getStrategiesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getStrategiesCount;
        protected int getStrategiesCount
        {
            get
            {
                return _getStrategiesCount;
            }
            set
            {
                if (!object.Equals(_getStrategiesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStrategiesCount", NewValue = value, OldValue = _getStrategiesCount };
                    _getStrategiesCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        dynamic _master;
        protected dynamic master
        {
            get
            {
                return _master;
            }
            set
            {
                if (!object.Equals(_master, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "master", NewValue = value, OldValue = _master };
                    _master = value;
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
            var dialogResult = await DialogService.OpenAsync<AddStrategy>("Add Strategy", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Cryptodb.ExportStrategiesToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "name,symbol,updated_time,version,exchange_type,id,position_type,status,ETag" }, $"Strategy");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Cryptodb.ExportStrategiesToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "name,symbol,updated_time,version,exchange_type,id,position_type,status,ETag" }, $"Strategy");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            AppState.IsBusy = true;

            try
            {
                var cryptodbGetStrategiesResult = await Cryptodb.GetStrategies(orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getStrategiesResult = cryptodbGetStrategiesResult.Value.AsODataEnumerable();

                getStrategiesCount = cryptodbGetStrategiesResult.Count;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetStrategiesException)
            {
            AppState.IsBusy = false;

                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Strategies" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowExpand(CryptobotUi.Models.Cryptodb.Strategy args)
        {
            AppState.IsBusy = true;

            master = args;

            try
            {
                var cryptodbGetStrategyConditionsResult = await Cryptodb.GetStrategyConditions(filter:$"strategy_id eq {args.id}");
                args.StrategyConditions = cryptodbGetStrategyConditionsResult.Value;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetStrategyConditionsException)
            {
            AppState.IsBusy = false;
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(CryptobotUi.Models.Cryptodb.Strategy args)
        {
            var dialogResult = await DialogService.OpenAsync<EditStrategy>("Edit Strategy", new Dictionary<string, object>() { {"id", args.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var cryptodbDeleteStrategyResult = await Cryptodb.DeleteStrategy(id:data.id);
                    if (cryptodbDeleteStrategyResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception cryptodbDeleteStrategyException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Strategy" });
            }
        }

        protected async System.Threading.Tasks.Task StrategyConditionAddButtonClick(MouseEventArgs args, dynamic data)
        {
            var dialogResult = await DialogService.OpenAsync<AddStrategyCondition>("Add Strategy Condition", new Dictionary<string, object>() { {"strategy_id", data.id} }, new DialogOptions(){ Width = $"{800}px" });
            await CreateStrategyCondition(dialogResult,data.id);

            await grid1.Reload();
        }

        protected async System.Threading.Tasks.Task StrategyConditionDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var cryptodbDeleteStrategyConditionResult = await Cryptodb.DeleteStrategyCondition(id:data.id);
                    if (cryptodbDeleteStrategyConditionResult != null)
                    {
                        await grid1.Reload();
                    }
                }
            }
            catch (System.Exception cryptodbDeleteStrategyConditionException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Strategy" });
            }
        }
    }
}
