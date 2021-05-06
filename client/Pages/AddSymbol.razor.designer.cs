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
    public partial class AddSymbolComponent : ComponentBase
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

        CryptobotUi.Models.Cryptodb.Symbol _symbol;
        protected CryptobotUi.Models.Cryptodb.Symbol symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                if (!object.Equals(_symbol, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "symbol", NewValue = value, OldValue = _symbol };
                    _symbol = value;
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
            symbol = new CryptobotUi.Models.Cryptodb.Symbol(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(CryptobotUi.Models.Cryptodb.Symbol args)
        {
            try
            {
                var cryptodbCreateSymbolResult = await Cryptodb.CreateSymbol(symbol:symbol);
                DialogService.Close(symbol);
            }
            catch (System.Exception cryptodbCreateSymbolException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Symbol!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
