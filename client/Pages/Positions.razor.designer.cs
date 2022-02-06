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
    public partial class PositionsComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.Position> grid0;

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

        IEnumerable<CryptobotUi.Models.Cryptodb.Position> _getPositionsResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.Position> getPositionsResult
        {
            get
            {
                return _getPositionsResult;
            }
            set
            {
                if (!object.Equals(_getPositionsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPositionsResult", NewValue = value, OldValue = _getPositionsResult };
                    _getPositionsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getPositionsCount;
        protected int getPositionsCount
        {
            get
            {
                return _getPositionsCount;
            }
            set
            {
                if (!object.Equals(_getPositionsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getPositionsCount", NewValue = value, OldValue = _getPositionsCount };
                    _getPositionsCount = value;
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

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Cryptodb.ExportPositionsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "signal_id,symbol,position_type,exchange_id,strategy_pair_name,signal_status,position_status,executed_buy_qty,pending_buy_qty,executed_sell_qty,pending_sell_qty,entry_price,close_price" }, $"Futures Positions");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Cryptodb.ExportPositionsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "signal_id,symbol,position_type,exchange_id,strategy_pair_name,signal_status,position_status,executed_buy_qty,pending_buy_qty,executed_sell_qty,pending_sell_qty,entry_price,close_price" }, $"Futures Positions");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            AppState.IsBusy = true;

            try
            {
                var cryptodbGetPositionsResult = await Cryptodb.GetPositions(filter:$@"(contains(symbol,""{search}"") or contains(position_type,""{search}"") or contains(strategy_pair_name,""{search}"") or contains(signal_status,""{search}"") or contains(position_status,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getPositionsResult = cryptodbGetPositionsResult.Value.AsODataEnumerable();

                getPositionsCount = cryptodbGetPositionsResult.Count;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetPositionsException)
            {
            AppState.IsBusy = false;

                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Positions" });
            }
        }
    }
}
