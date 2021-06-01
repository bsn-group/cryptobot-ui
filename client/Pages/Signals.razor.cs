using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using CryptobotUi.Client.Model;
using CryptobotUi.Models.Cryptodb;
using CryptobotUi.Client.Model.Extensions;

namespace CryptobotUi.Pages
{
    public partial class SignalsComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        private int SignalIdRouteParameter 
        {
            get 
            {
                Int32.TryParse(SignalId, out int signalId);
                return signalId;
            }
        }

        protected async System.Threading.Tasks.Task OnGridRowExpand(CryptobotUi.Models.Cryptodb.FuturesSignal args)
        {
            try
            {
                AppState.IsBusy = true;

                // master = args;

                var commands = await Cryptodb.GetFuturesSignalCommands(filter:$"signal_id eq {args.signal_id}");
                args.FuturesSignalCommands = 
                    commands.Value
                    .Select(c => {
                        c.action_date_time = c.action_date_time.ToLocalTime();
                        c.request_date_time = c.request_date_time.ToLocalTime();
                        return c;
                    });
                
                var orders = await Cryptodb.GetExchangeOrders(filter:$"signal_id eq {args.signal_id}");
                args.ExchangeOrders = 
                    orders.Value
                    .Select(o => {
                        o.created_time = o.created_time.ToLocalTime();
                        o.updated_time = o.updated_time.ToLocalTime();
                        return o;
                    });
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load commands and orders: {ex.Message}" });
            }
            finally
            {
                AppState.IsBusy = false;
            }
        }

        protected IEnumerable<FuturesSignal> getFuturesSignalsResult;
        protected int getFuturesSignalsCount;

        protected async System.Threading.Tasks.Task OnGridLoadData(LoadDataArgs args)
        {
            try
            {
                AppState.IsBusy = true;

                args.OrderBy = string.IsNullOrWhiteSpace(args.OrderBy) ? "signal_id desc" : args.OrderBy;

                args.Filter = 
                    string.IsNullOrWhiteSpace(args.Filter) && this.SignalIdRouteParameter > 0
                    ? $"signal_id eq {SignalIdRouteParameter}"
                    : args.Filter;

                var cryptodbGetFuturesSignalsResult = await Cryptodb.GetFuturesSignals(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", expand:$"Exchange", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getFuturesSignalsResult = 
                    cryptodbGetFuturesSignalsResult.Value
                    .Select(c => {
                        c.created_date_time = c.created_date_time.ToLocalTime();
                        c.updated_date_time = c.updated_date_time.ToLocalTime();
                        return c;
                    })
                    .AsODataEnumerable();
                getFuturesSignalsCount = cryptodbGetFuturesSignalsResult.Count;

                if (getFuturesSignalsCount > 0)
                {
                    // expand the row
                    var signal = getFuturesSignalsResult.FirstOrDefault(s => s.signal_id == SignalIdRouteParameter);
                    if (signal != null) 
                    {
                        await grid0.ExpandRow(signal);
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage{
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to load FuturesSignals: {ex}"
                });
                JSRuntime.Log($"Error loading signals: {ex}. Type of signalid {this.SignalId?.GetType()?.Name}");
            }
            finally
            {

                AppState.IsBusy = false;
            }
        }
    }
}
