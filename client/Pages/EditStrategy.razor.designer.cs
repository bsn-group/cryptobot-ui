﻿using System;
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
    public partial class EditStrategyComponent : ComponentBase
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
        public dynamic id { get; set; }

        bool _hasChanges;
        protected bool hasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (!object.Equals(_hasChanges, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "hasChanges", NewValue = value, OldValue = _hasChanges };
                    _hasChanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (!object.Equals(_canEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "canEdit", NewValue = value, OldValue = _canEdit };
                    _canEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            hasChanges = false;

            canEdit = true;

            var cryptodbGetStrategyByidResult = await Cryptodb.GetStrategyByid(id:id);
            strategy = cryptodbGetStrategyByidResult;

            canEdit = cryptodbGetStrategyByidResult != null;

            await LoadInitData();
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(CryptobotUi.Models.Cryptodb.Strategy args)
        {
            try
            {
                var cryptodbUpdateStrategyResult = await Cryptodb.UpdateStrategy(id:id, strategy:strategy);
                if (cryptodbUpdateStrategyResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(strategy);
                }

                hasChanges = cryptodbUpdateStrategyResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception cryptodbUpdateStrategyException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Strategy" });
            }
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
