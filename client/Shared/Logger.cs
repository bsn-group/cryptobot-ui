using Microsoft.JSInterop;

namespace CryptobotUi
{
    public static class LoggerExtensions
    {
        public static void Log(this IJSRuntime jsRuntime, params object[] args) 
        {
            jsRuntime.InvokeAsync<string>("console.log", args);
        }
    }
    
}