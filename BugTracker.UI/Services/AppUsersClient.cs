using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class AppUsersClient : BaseApiClient<AppUser>
    {
        private string _endpoint = "appusers";

        public AppUsersClient(HttpClient httpClient) : base(httpClient, "appusers")
        {
        }

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            return await _httpClient.GetFromJsonAsync<AppUser>($"api/{_endpoint}/by-email/{email}");
        }
    }
}
