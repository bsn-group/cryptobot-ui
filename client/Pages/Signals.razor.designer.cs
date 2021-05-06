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
    public partial class SignalsComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.FuturesSignal> grid0;
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.FuturesSignalCommand> grid1;

        IEnumerable<CryptobotUi.Models.Cryptodb.FuturesSignal> _getFuturesSignalsResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.FuturesSignal> getFuturesSignalsResult
        {
            get
            {
                return _getFuturesSignalsResult;
            }
            set
            {
                if (!object.Equals(_getFuturesSignalsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFuturesSignalsResult", NewValue = value, OldValue = _getFuturesSignalsResult };
                    _getFuturesSignalsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getFuturesSignalsCount;
        protected int getFuturesSignalsCount
        {
            get
            {
                return _getFuturesSignalsCount;
            }
            set
            {
                if (!object.Equals(_getFuturesSignalsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFuturesSignalsCount", NewValue = value, OldValue = _getFuturesSignalsCount };
                    _getFuturesSignalsCount = value;
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

        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            AppState.IsBusy = true;

            args.OrderBy = "signal_id desc";

            try
            {
                var cryptodbGetFuturesSignalsResult = await Cryptodb.GetFuturesSignals(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", expand:$"Exchange", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getFuturesSignalsResult = cryptodbGetFuturesSignalsResult.Value.AsODataEnumerable();

                getFuturesSignalsCount = cryptodbGetFuturesSignalsResult.Count;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetFuturesSignalsException)
            {
            AppState.IsBusy = false;

                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load FuturesSignals" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowExpand(CryptobotUi.Models.Cryptodb.FuturesSignal args)
        {
            AppState.IsBusy = true;

            master = args;

            try
            {
                var cryptodbGetFuturesSignalCommandsResult = await Cryptodb.GetFuturesSignalCommands(filter:$"signal_id eq {args.signal_id}");
                args.FuturesSignalCommands = cryptodbGetFuturesSignalCommandsResult.Value;

                AppState.IsBusy = false;
            }
            catch (System.Exception cryptodbGetFuturesSignalCommandsException)
            {
            AppState.IsBusy = false;
            }
        }
    }
}
