using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using CryptobotUi.Client.Model;
using Microsoft.AspNetCore.Components;
using CryptobotUi.Models.Cryptodb;

namespace CryptobotUi.Pages
{
    public partial class FuturesPnlsComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        protected IEnumerable<CryptobotUi.Client.Model.FuturesPnlViewModel> FuturesPnls { get; set; }
        protected int FuturesPnlsCount { get; set; }

        private FuturesPnlViewModel ToFuturesPnlVM(FuturesPnl pnl)
        {
            return new FuturesPnlViewModel {
                close_price = pnl.close_price,
                entry_price = pnl.entry_price,
                ETag = pnl.ETag,
                entry_time = pnl.entry_time,
                exchange_id = pnl.exchange_id,
                executed_buy_qty = pnl.executed_buy_qty,
                executed_sell_qty = pnl.executed_sell_qty,
                exit_time = pnl.exit_time,
                pending_buy_qty = pnl.pending_buy_qty,
                pending_sell_qty = pnl.pending_sell_qty,
                pnl = pnl.pnl,
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

                var pnlsResult = await Cryptodb.GetFuturesPnls(filter:$@"(contains(symbol,""{search}"") or contains(position_type,""{search}"") or contains(strategy_pair_name,""{search}"") or contains(signal_status,""{search}"") or contains(position_status,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);

                FuturesPnls = pnlsResult.Value.Select(ToFuturesPnlVM).AsODataEnumerable();
                FuturesPnlsCount = pnlsResult.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error,Summary = $"Error", Detail = $"Unable to load FuturesPnls: {ex.Message}" });
            }
            finally 
            {
                AppState.IsBusy = false;
            }
        }

        protected async Task OnPnlGridRowExpand(FuturesPnlViewModel pnl)
        {
            await OnCommandsGridLoadData(new LoadDataArgs{
                Top = 50,
                Skip = 0
            }, pnl); // looks like commandsGrid is null and so we can't call Reload on it
        }

        protected async Task OnCommandsGridLoadData(LoadDataArgs args, FuturesPnlViewModel pnl)
        {
            try 
            {
                AppState.IsBusy = true;

                var signalId = pnl.signal_id;
                args.Filter = string.IsNullOrWhiteSpace(args.Filter) ? $"signal_id eq {signalId}" : args.Filter;
                var commands = await Cryptodb.GetFuturesSignalCommands(filter: $"{args.Filter}"); //, orderby: $"{args.OrderBy}", expand: $"ExchangeOrder", top: args.Top, skip: args.Skip, count: args.Top != null && args.Skip != null);

                pnl.Commands = commands.Value;
                //pnl.CommandCount = commands.Count;
                JSRuntime.Log("Added commands to pnl: " + pnl.CommandCount);
            }
            catch (Exception ex) 
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error,Summary = $"Error", Detail = $"Unable to load SignalCommands: {ex.Message}" });
                JSRuntime.Log($"CommandsGridLoadData Error: {ex}");
            }
            finally 
            {
                AppState.IsBusy = false;
            }
        }
    }
}
