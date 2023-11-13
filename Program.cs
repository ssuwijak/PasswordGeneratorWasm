using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PasswordGeneratorWasm;
using PasswordGeneratorWasm.Shared;
using Cb = PasswordGeneratorWasm.Shared.Clipboard;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<Cb.Meziantou.ClipboardService>();
builder.Services.AddScoped<Cb.CodeMaze.IClipBoardService, Cb.CodeMaze.ClipboardService>();

await builder.Build().RunAsync();
