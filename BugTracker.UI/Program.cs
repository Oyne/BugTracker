using BugTracker.UI;
using BugTracker.UI.Services.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7132") });
builder.Services.AddScoped<BugTracker.UI.Services.BugClient>();
builder.Services.AddScoped<BugTracker.UI.Services.PriorityClient>();
builder.Services.AddScoped<BugTracker.UI.Services.StatusClient>();
builder.Services.AddScoped<BugTracker.UI.Services.CategoryClient>();
builder.Services.AddScoped<BugTracker.UI.Services.AppUserClient>();
builder.Services.AddScoped<BugTracker.UI.Services.RoleClient>();
builder.Services.AddScoped<BugTracker.UI.Services.StorageService>();
builder.Services.AddSingleton<BugTracker.UI.Services.ToastService>();
builder.Services.AddScoped<BugTracker.UI.Services.NavigationService>();
builder.Services.AddScoped<BugTracker.UI.Services.State.UserState>();


var host = builder.Build();

var userState = host.Services.GetRequiredService<UserState>();
await userState.LoadUserAsync();

await host.RunAsync();

await builder.Build().RunAsync();
