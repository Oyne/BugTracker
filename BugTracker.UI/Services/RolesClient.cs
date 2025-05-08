using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class RolesClient : BaseApiClient<Role>
    {
        private string _endpoint = "roles";

        public RolesClient(HttpClient httpClient) : base(httpClient, "roles")
        {
        }

        public async Task<List<AppUser>?> GetUsersWithRole(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<AppUser>>($"api/{_endpoint}/{id}/users");
        }
    }
}
