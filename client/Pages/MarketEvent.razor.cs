using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using CryptobotUi.Client.Model;
using Microsoft.AspNetCore.Components;

namespace CryptobotUi.Pages
{
    public partial class MarketEventComponent
    {
        [Inject]
        protected AppState AppState { get; set; }

        protected async Task RaiseMarketEvent(CryptobotUi.Models.Cryptodb.MarketEvent evt)
        {
            
        }

        IEnumerable<CryptobotUi.Models.Cryptodb.MarketEvent> _getMarketEventsResult;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.MarketEvent> getMarketEventsResult
        {
            get
            {
                return _getMarketEventsResult;
            }
            set
            {
                if (!object.Equals(_getMarketEventsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMarketEventsResult", NewValue = value, OldValue = _getMarketEventsResult };
                    _getMarketEventsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getMarketEventsCount;
        protected int getMarketEventsCount
        {
            get
            {
                return _getMarketEventsCount;
            }
            set
            {
                if (!object.Equals(_getMarketEventsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getMarketEventsCount", NewValue = value, OldValue = _getMarketEventsCount };
                    _getMarketEventsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected async System.Threading.Tasks.Task OnGridLoadData(LoadDataArgs args)
        {
            args.OrderBy = string.IsNullOrWhiteSpace(args.OrderBy) ? "event_time desc" : args.OrderBy;

            try
            {
                var cryptodbGetMarketEventsResult = await Cryptodb.GetMarketEvents(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getMarketEventsResult = 
                    cryptodbGetMarketEventsResult.Value
                    .Select(e => {
                        e.event_time = e.event_time.ToLocalTime();
                        return e;
                    })
                    .AsODataEnumerable();

                getMarketEventsCount = cryptodbGetMarketEventsResult.Count;
            }
            catch (System.Exception cryptodbGetMarketEventsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load MarketEvents" });
            }
        }
    }
}
