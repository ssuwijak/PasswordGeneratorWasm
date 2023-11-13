using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace PasswordGeneratorWasm.Shared.Clipboard.Meziantou
{
    // https://www.meziantou.net/copying-text-to-clipboard-in-a-blazor-application.htm
    public sealed class ClipboardService
    {
        // sealed = noninheritable

        private readonly IJSRuntime _jsRuntime;

        public ClipboardService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public ValueTask<string> ReadTextAsync()
        {
            return _jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
        }

        public ValueTask WriteTextAsync(string text)
        {
            return _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}