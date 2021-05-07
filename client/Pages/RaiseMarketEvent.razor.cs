using CryptobotUi.Models.Shared;
using Microsoft.AspNetCore.Components;

namespace CryptobotUi.Pages
{
    public partial class RaiseMarketEventComponent
    {
        [Parameter]
        public MarketEvent MarketEvent { get => this._marketevent; set => this._marketevent = value; }
    }
}
