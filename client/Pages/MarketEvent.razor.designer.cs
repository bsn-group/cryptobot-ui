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
    public partial class MarketEventComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.MarketEvent> grid0;

        IEnumerable<CryptobotUi.Models.Cryptodb.MarketEvent> _getMarketEventsResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.MarketEvent> getMarketEventsResult
        {
            get
            {
                return _getMarketEventsResult;
            }
            set
            {
                if (!object.Equals(_getMarketEventsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMarketEventsResult", NewValue = value, OldValue = _getMarketEventsResult };
                    _getMarketEventsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getMarketEventsCount;
        protected int getMarketEventsCount
        {
            get
            {
                return _getMarketEventsCount;
            }
            set
            {
                if (!object.Equals(_getMarketEventsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMarketEventsCount", NewValue = value, OldValue = _getMarketEventsCount };
                    _getMarketEventsCount = value;
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

        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddMarketEvent>("Add Market Event", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var cryptodbGetMarketEventsResult = await Cryptodb.GetMarketEvents(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getMarketEventsResult = cryptodbGetMarketEventsResult.Value.AsODataEnumerable();

                getMarketEventsCount = cryptodbGetMarketEventsResult.Count;
            }
            catch (System.Exception cryptodbGetMarketEventsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load MarketEvents" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(CryptobotUi.Models.Cryptodb.MarketEvent args)
        {
            var dialogResult = await DialogService.OpenAsync<EditMarketEvent>("Edit Market Event", new Dictionary<string, object>() { {"id", args.id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var cryptodbDeleteMarketEventResult = await Cryptodb.DeleteMarketEvent(id:data.id);
                    if (cryptodbDeleteMarketEventResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception cryptodbDeleteMarketEventException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete MarketEvent" });
            }
        }
    }
}
