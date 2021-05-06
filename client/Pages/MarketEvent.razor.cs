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
    }
}
