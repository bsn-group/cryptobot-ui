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
    public partial class ExchangesComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.Exchange> grid0;

        IEnumerable<CryptobotUi.Models.Cryptodb.Exchange> _getExchangesResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.Exchange> getExchangesResult
        {
            get
            {
                return _getExchangesResult;
            }
            set
            {
                if (!object.Equals(_getExchangesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getExchangesResult", NewValue = value, OldValue = _getExchangesResult };
                    _getExchangesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getExchangesCount;
        protected int getExchangesCount
        {
            get
            {
                return _getExchangesCount;
            }
            set
            {
                if (!object.Equals(_getExchangesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getExchangesCount", NewValue = value, OldValue = _getExchangesCount };
                    _getExchangesCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            AppState.IsBusy = true;

            try
            {
                var cryptodbGetExchangesResult = await Cryptodb.GetExchanges(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getExchangesResult = cryptodbGetExchangesResult.Value.AsODataEnumerable();

                getExchangesCount = cryptodbGetExchangesResult.Count;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetExchangesException)
            {
            AppState.IsBusy = false;
            }
        }
    }
}
