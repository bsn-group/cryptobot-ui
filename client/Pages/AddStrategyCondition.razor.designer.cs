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
    public partial class AddStrategyConditionComponent : ComponentBase
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

        [Parameter]
        public dynamic strategy_id { get; set; }

        CryptobotUi.Models.Cryptodb.StrategyCondition _strategycondition;
        protected CryptobotUi.Models.Cryptodb.StrategyCondition strategycondition
        {
            get
            {
                return _strategycondition;
            }
            set
            {
                if (!object.Equals(_strategycondition, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strategycondition", NewValue = value, OldValue = _strategycondition };
                    _strategycondition = value;
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
            strategycondition = new CryptobotUi.Models.Cryptodb.StrategyCondition(){};
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            DialogService.Close(strategycondition);
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task FromJsonButtonClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<LoadMarketEventJson>("Load Market Event JSON", null, new DialogOptions(){ Width = $"{800}px" });
            LoadStrategyDataFromMarketEvent(dialogResult);
        }
    }
}
