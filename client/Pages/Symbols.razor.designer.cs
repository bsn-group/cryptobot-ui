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
    public partial class SymbolsComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.Symbol> grid0;

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

        IEnumerable<CryptobotUi.Models.Cryptodb.Symbol> _getSymbolsResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.Symbol> getSymbolsResult
        {
            get
            {
                return _getSymbolsResult;
            }
            set
            {
                if (!object.Equals(_getSymbolsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSymbolsResult", NewValue = value, OldValue = _getSymbolsResult };
                    _getSymbolsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getSymbolsCount;
        protected int getSymbolsCount
        {
            get
            {
                return _getSymbolsCount;
            }
            set
            {
                if (!object.Equals(_getSymbolsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSymbolsCount", NewValue = value, OldValue = _getSymbolsCount };
                    _getSymbolsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddSymbol>("Add Symbol", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Cryptodb.ExportSymbolsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "name" }, $"Symbols");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Cryptodb.ExportSymbolsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "name" }, $"Symbols");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            AppState.IsBusy = true;

            try
            {
                var cryptodbGetSymbolsResult = await Cryptodb.GetSymbols(filter:$@"(contains(name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getSymbolsResult = cryptodbGetSymbolsResult.Value.AsODataEnumerable();

                getSymbolsCount = cryptodbGetSymbolsResult.Count;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetSymbolsException)
            {
            AppState.IsBusy = false;

                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Symbols" });
            }
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var cryptodbDeleteSymbolResult = await Cryptodb.DeleteSymbol(name:$"{data.name}");
                    if (cryptodbDeleteSymbolResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception cryptodbDeleteSymbolException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Symbol" });
            }
        }
    }
}
