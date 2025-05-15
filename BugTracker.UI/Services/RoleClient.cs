using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class RoleClient : BaseApiClient<Role>
    {
        private const string _endpoint = "roles";

        public RoleClient(HttpClient httpClient) : base(httpClient, _endpoint)
        {
        }

        public async Task<List<AppUser>?> GetUsersWithRoleAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<AppUser>>($"api/{_endpoint}/{id}/users");
        }
    }
}
