using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using CryptobotUi.Client.Model;

namespace CryptobotUi.Pages
{
    public partial class SignalsComponent
    {
        [Inject]
        protected AppState AppState { get; set; }
    }
}
