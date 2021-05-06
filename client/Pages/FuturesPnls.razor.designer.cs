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
    public partial class FuturesPnlsComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Client.Model.FuturesPnlViewModel> pnlDataGrid;
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.FuturesSignalCommand> grid0;

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
                await Cryptodb.ExportFuturesPnlsToCSV(new Query() { Filter = $@"{pnlDataGrid.Query.Filter}", OrderBy = $"{pnlDataGrid.Query.OrderBy}", Expand = "", Select = "signal_id,symbol,position_type,exchange_id,strategy_pair_name,signal_status,position_status,executed_buy_qty,pending_buy_qty,executed_sell_qty,pending_sell_qty,entry_price,close_price,pnl,pnl_percent" }, $"Futures Pnls");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Cryptodb.ExportFuturesPnlsToExcel(new Query() { Filter = $@"{pnlDataGrid.Query.Filter}", OrderBy = $"{pnlDataGrid.Query.OrderBy}", Expand = "", Select = "signal_id,symbol,position_type,exchange_id,strategy_pair_name,signal_status,position_status,executed_buy_qty,pending_buy_qty,executed_sell_qty,pending_sell_qty,entry_price,close_price,pnl,pnl_percent" }, $"Futures Pnls");

            }
        }

        protected async System.Threading.Tasks.Task RefreshButtonClick(MouseEventArgs args)
        {
            await pnlDataGrid.Reload();
        }

        protected async System.Threading.Tasks.Task PnlDataGridLoadData(LoadDataArgs args)
        {
            await OnPnlGridLoadData(args);
        }

        protected async System.Threading.Tasks.Task PnlDataGridRowExpand(CryptobotUi.Client.Model.FuturesPnlViewModel args)
        {
            await OnPnlGridRowExpand(args);
        }
    }
}
