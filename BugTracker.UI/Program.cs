using BugTracker.UI;
using BugTracker.UI.Services.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7132") });
builder.Services.AddScoped<BugTracker.UI.Services.BugsClient>();
builder.Services.AddScoped<BugTracker.UI.Services.PrioritiesClient>();
builder.Services.AddScoped<BugTracker.UI.Services.StatusesClient>();
builder.Services.AddScoped<BugTracker.UI.Services.CategoriesClient>();
builder.Services.AddScoped<BugTracker.UI.Services.AppUsersClient>();
builder.Services.AddScoped<BugTracker.UI.Services.RolesClient>();
builder.Services.AddScoped<BugTracker.UI.Services.StorageService>();
builder.Services.AddScoped<BugTracker.UI.Services.NavigationService>();
builder.Services.AddScoped<BugTracker.UI.Services.State.UserState>();
builder.Services.AddSingleton<BugTracker.UI.Services.ToastService>();

var host = builder.Build();

var userState = host.Services.GetRequiredService<UserState>();
await userState.LoadUserAsync();

await host.RunAsync();

await builder.Build().RunAsync();
