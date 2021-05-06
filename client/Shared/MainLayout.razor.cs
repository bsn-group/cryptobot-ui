using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using CryptobotUi.Client.Model;

namespace CryptobotUi.Layouts
{
    public partial class MainLayoutComponent
    {
        [Inject]
        protected AppState AppState { get; set; }
        protected bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            AppState.PropertyChanged += (_, _) => StateHasChanged();
        }
    }
}
