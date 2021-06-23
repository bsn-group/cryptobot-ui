using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using CryptobotUi.Client.Model;

namespace CryptobotUi.Pages
{
    public partial class EditStrategyComponent
    {
        protected IEnumerable<LookupValue> positionTypes =>
            Enum.GetNames<PositionTypes>()
                .Select(p => new LookupValue { Name = p });

        protected IEnumerable<LookupValue> exchangeTypes =>
            Enum.GetNames<ExchangeTypes>()
                .Select(p => new LookupValue { Name = p });

        protected IEnumerable<LookupValue> statuses =>
            new[] {"ACTIVE", "INACTIVE"}
                .Select(p => new LookupValue { Name = p });

        IEnumerable<CryptobotUi.Models.Cryptodb.Symbol> _symbols;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.Symbol> symbols
        {
            get
            {
                return _symbols;
            }
            set
            {
                if (!object.Equals(_symbols, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = nameof(symbols), NewValue = value, OldValue = _symbols };
                    _symbols = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<CryptobotUi.Models.Cryptodb.Exchange> _exchanges;
        protected IEnumerable<CryptobotUi.Models.Cryptodb.Exchange> exchanges
        {
            get
            {
                return _exchanges;
            }
            set
            {
                if (!object.Equals(_exchanges, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = nameof(exchanges), NewValue = value, OldValue = _exchanges };
                    _exchanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected async Task LoadExchanges()
        {
            try
            {
                var result = await Cryptodb.GetExchanges();
                exchanges = result.Value.AsODataEnumerable();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load exchanges! {ex.Message}" });
                JSRuntime.Log($"Error loading exchanges: {ex}");
            }
        }

        protected async Task LoadSymbols()
        {
            try
            {
                var result = await Cryptodb.GetSymbols();
                symbols = result.Value.AsODataEnumerable();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load symbols! {ex.Message}" });
                JSRuntime.Log($"Error loading symbols: {ex}");
            }
        }

        protected Task LoadInitData()
        {
            var tasks = new[] {
                LoadSymbols(),
                LoadExchanges()
            };
            return Task.WhenAll(tasks);
        }
    }
}
