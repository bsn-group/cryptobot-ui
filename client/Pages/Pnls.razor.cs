using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using CryptobotUi.Client.Model;
using Microsoft.AspNetCore.Components;
using CryptobotUi.Models.Cryptodb;
using CryptobotUi.Client.Model.Extensions;

namespace CryptobotUi.Pages
{
    public partial class PnlsComponent
    {
        private IEnumerable<PnlViewModel> pnls;
        private int pnlsCount;

        [Inject]
        protected AppState AppState { get; set; }

        protected IEnumerable<CryptobotUi.Client.Model.PnlViewModel> Pnls
        {
            get => pnls;
            set
            {
                if (!object.Equals(pnls, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = nameof(Pnls), NewValue = value, OldValue = pnls };
                    pnls = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected int PnlsCount
        {
            get => pnlsCount;
            set
            {
                if (!object.Equals(pnlsCount, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = nameof(PnlsCount), NewValue = value, OldValue = pnlsCount };
                    pnlsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        private CryptobotUi.Client.Model.PnlViewModel selectedPnl;
        public CryptobotUi.Client.Model.PnlViewModel SelectedPnl
        {
            get => selectedPnl;
            set
            {
                if (!object.Equals(selectedPnl, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = nameof(SelectedPnl), NewValue = value, OldValue = selectedPnl };
                    selectedPnl = value;
                    OnPropertyChanged(args);
                    Reload();
                }
                selectedPnl = value;
            }
        }


        private PnlViewModel ToPnlVM(Pnl pnl)
        {
            return new PnlViewModel
            {
                close_price = pnl.close_price,
                entry_price = pnl.entry_price,
                ETag = pnl.ETag,
                entry_time = pnl.entry_time.ToLocalTime(),
                exchange_id = pnl.exchange_id,
                executed_buy_qty = pnl.executed_buy_qty,
                executed_sell_qty = pnl.executed_sell_qty,
                exit_time = pnl.exit_time.ToLocalTime(),
                pending_buy_qty = pnl.pending_buy_qty,
                pending_sell_qty = pnl.pending_sell_qty,
                pnl1 = pnl.pnl1,
                pnl_percent = pnl.pnl_percent,
                position_status = pnl.position_status,
                position_type = pnl.position_type,
                signal_id = pnl.signal_id,
                signal_status = pnl.signal_status,
                strategy_pair_name = pnl.strategy_pair_name,
                symbol = pnl.symbol
            };
        }

        protected async System.Threading.Tasks.Task OnPnlGridLoadData(LoadDataArgs args)
        {
            try
            {
                AppState.IsBusy = true;

                args.OrderBy = string.IsNullOrWhiteSpace(args.OrderBy) ? "signal_id desc" : args.OrderBy;

                var pnlsResult = await Cryptodb.GetPnls(filter: $@"(contains(symbol,""{search}"") or contains(position_type,""{search}"") or contains(strategy_pair_name,""{search}"") or contains(signal_status,""{search}"") or contains(position_status,""{search}"")) and {(string.IsNullOrEmpty(args.Filter) ? "true" : args.Filter)}", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count: args.Top != null && args.Skip != null);

                Pnls = pnlsResult.Value.Select(ToPnlVM).AsODataEnumerable();
                PnlsCount = pnlsResult.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Pnls: {ex.Message}" });
            }
            finally
            {
                AppState.IsBusy = false;
            }
        }

        protected async Task Export(Radzen.Blazor.RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Cryptodb.ExportPnlsToCSV(new Query() { Filter = $@"{pnlDataGrid.Query.Filter}", OrderBy = $"{pnlDataGrid.Query.OrderBy}", Expand = "", Select = "signal_id,symbol,position_type,exchange_id,strategy_pair_name,signal_status,position_status,executed_buy_qty,pending_buy_qty,executed_sell_qty,pending_sell_qty,entry_price,close_price,pnl,pnl_percent,entry_time,exit_time" }, $"Pnls");
            }

            if (args == null || args.Value == "xlsx")
            {
                await Cryptodb.ExportPnlsToExcel(new Query() { Filter = $@"{pnlDataGrid.Query.Filter}", OrderBy = $"{pnlDataGrid.Query.OrderBy}", Expand = "", Select = "signal_id,symbol,position_type,exchange_id,strategy_pair_name,signal_status,position_status,executed_buy_qty,pending_buy_qty,executed_sell_qty,pending_sell_qty,entry_price,close_price,pnl,pnl_percent,entry_time,exit_time" }, $"Pnls");
            }
        }

        protected async Task OnPnlGridRowExpand(PnlViewModel pnl)
        {
            await OnCommandsGridLoadData(new LoadDataArgs
            {
                Top = 50,
                Skip = 0
            }, pnl); // looks like commandsGrid is null at this point - so we can't call .Reload() on it directly
        }

        protected async Task OnCommandsGridLoadData(LoadDataArgs args, PnlViewModel pnl)
        {
            try
            {
                AppState.IsBusy = true;

                SelectedPnl = pnl;

                var signalId = pnl.signal_id;
                args.Filter = string.IsNullOrWhiteSpace(args.Filter) ? $"signal_id eq {signalId}" : args.Filter;
                var commands = await Cryptodb.GetSignalCommands(filter: $"{args.Filter}"); //, orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count: args.Top != null && args.Skip != null);

                pnl.Commands = commands.Value;
                pnl.CommandCount = commands.Count;
                JSRuntime.Log("Added commands to pnl: " + pnl.Commands.Count());
                Reload();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load SignalCommands: {ex.Message}" });
                JSRuntime.Log($"CommandsGridLoadData Error: {ex}");
            }
            finally
            {
                AppState.IsBusy = false;
            }
        }
    }
}
