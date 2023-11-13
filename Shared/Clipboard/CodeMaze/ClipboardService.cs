using Microsoft.JSInterop;
using PasswordGeneratorWasm.Pages;
using static System.Net.Mime.MediaTypeNames;

namespace PasswordGeneratorWasm.Shared.Clipboard.CodeMaze
{
	// https://code-maze.com/copy-to-clipboard-in-blazor-webassembly/
	public interface IClipBoardService
    {
        Task CopyToClipBoard(string text);
    }
    public class ClipboardService : IClipBoardService
    {
        private readonly IJSRuntime _jsInterop;
        public ClipboardService(IJSRuntime jsInterop)
        {
            _jsInterop = jsInterop;
        }
        public async Task CopyToClipBoard(string text)
        {
			if (string.IsNullOrEmpty(text))
				text = "";

            await _jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);
		}
    }


}
