using CryptobotUi.Client.Model;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CryptobotUi
{
    partial class Program
    {
        static partial void OnConfigureBuilder(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(new AppState());
        }
    }
}