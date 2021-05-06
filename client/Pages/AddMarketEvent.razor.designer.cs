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
    public partial class AddMarketEventComponent : ComponentBase
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

        CryptobotUi.Models.Cryptodb.MarketEvent _marketevent;
        protected CryptobotUi.Models.Cryptodb.MarketEvent marketevent
        {
            get
            {
                return _marketevent;
            }
            set
            {
                if (!object.Equals(_marketevent, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "marketevent", NewValue = value, OldValue = _marketevent };
                    _marketevent = value;
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
            marketevent = new CryptobotUi.Models.Cryptodb.MarketEvent(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(CryptobotUi.Models.Cryptodb.MarketEvent args)
        {
            try
            {
                var cryptodbCreateMarketEventResult = await Cryptodb.CreateMarketEvent(marketEvent:marketevent);
                DialogService.Close(marketevent);
            }
            catch (System.Exception cryptodbCreateMarketEventException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new MarketEvent!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
