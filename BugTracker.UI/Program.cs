using BugTracker.UI;
using BugTracker.UI.Services;
using BugTracker.UI.Services.State;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
string env = builder.HostEnvironment.Environment;

string configFileName = env switch
{
    "Development" => "appsettings.Development.json",
    "Docker" => "appsettings.Docker.json",
    _ => "appsettings.json"
};

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<Dictionary<string, string>>(configFileName);

string apiBaseUrl = config != null && config.ContainsKey("ApiBaseUrl")
    ? config["ApiBaseUrl"]
    : "https://localhost:7132/"; // fallback

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
builder.Services.AddScoped<BugsClient>();
builder.Services.AddScoped<PrioritiesClient>();
builder.Services.AddScoped<StatusesClient>();
builder.Services.AddScoped<CategoriesClient>();
builder.Services.AddScoped<AppUsersClient>();
builder.Services.AddScoped<RolesClient>();
builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<NavigationService>();
builder.Services.AddScoped<UserState>();
builder.Services.AddSingleton<ToastService>();
builder.Services.AddSingleton<LoaderService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var host = builder.Build();

var userState = host.Services.GetRequiredService<UserState>();
await userState.LoadUserAsync();

await host.RunAsync();