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
    public partial class AddStrategyComponent : ComponentBase
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
        protected RadzenGrid<CryptobotUi.Models.Cryptodb.StrategyCondition> conditionsGrid;

        CryptobotUi.Models.Cryptodb.Strategy _strategy;
        protected CryptobotUi.Models.Cryptodb.Strategy strategy
        {
            get
            {
                return _strategy;
            }
            set
            {
                if (!object.Equals(_strategy, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strategy", NewValue = value, OldValue = _strategy };
                    _strategy = value;
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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            strategy = new CryptobotUi.Models.Cryptodb.Strategy(){};

            var cryptodbGetSymbolsResult = await Cryptodb.GetSymbols();
            getSymbolsResult = cryptodbGetSymbolsResult.Value.AsODataEnumerable();
        }

        protected async System.Threading.Tasks.Task Form0Submit(CryptobotUi.Models.Cryptodb.Strategy args)
        {
            await CreateStrategy();
        }

        protected async System.Threading.Tasks.Task SymbolChange(dynamic args)
        {
            SetName();
        }

        protected async System.Threading.Tasks.Task ExchangeTypeChange(dynamic args)
        {
            SetName();
        }

        protected async System.Threading.Tasks.Task PositionTypeChange(dynamic args)
        {
            SetName();
        }

        protected async System.Threading.Tasks.Task AddConditionButtonClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddStrategyCondition>("Add Strategy Condition", null, new DialogOptions(){ Width = $"{800}px" });
            AddCondition(dialogResult);
        }

        protected async System.Threading.Tasks.Task Button3Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
