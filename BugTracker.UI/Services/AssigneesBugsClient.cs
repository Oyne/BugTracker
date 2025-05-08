using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class AssigneesBugsClient
    {
        private string _endpoint = "assigneesbugs";
        private HttpClient _httpClient;

        public AssigneesBugsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Bug>?> GetBugsByAssigneeId(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Bug>>($"api/{_endpoint}/assignee/{id}");
        }

        public async Task<List<AppUser>?> GetAssigneesByBugId(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<AppUser>>($"api/{_endpoint}/bug/{id}");
        }

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            return await _httpClient.GetFromJsonAsync<AppUser>($"api/{_endpoint}/by-email/{email}");
        }

        public async Task<HttpResponseMessage> AssignBugToUser(AssigneeBug assigneeBug)
        {
            return await _httpClient.PostAsJsonAsync($"api/{_endpoint}", assigneeBug);
        }

        public async Task<HttpResponseMessage> Unassign(int id)
        {
            return await _httpClient.DeleteAsync($"api/{_endpoint}/{id}");
        }
    }
}
