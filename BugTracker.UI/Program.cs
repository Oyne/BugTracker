using BugTracker.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7132") });
builder.Services.AddScoped<BugTracker.UI.Services.BugClient>();
builder.Services.AddScoped<BugTracker.UI.Services.PrioritiesClient>();
builder.Services.AddScoped<BugTracker.UI.Services.StatusesClient>();
builder.Services.AddScoped<BugTracker.UI.Services.CategoriesClient>();

await builder.Build().RunAsync();
