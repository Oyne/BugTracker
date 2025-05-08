using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class StatusesClient : BaseApiClient<Status>
    {
        private string _endpoint = "statuses";

        public StatusesClient(HttpClient httpClient) : base(httpClient, "statuses")
        {
        }

        public async Task<List<Bug>?> GetBugsWithStatus(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Bug>>($"api/{_endpoint}/{id}/bugs");
        }
    }
}
