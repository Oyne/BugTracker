using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class PrioritiesClient : BaseApiClient<Priority>
    {
        private string _endpoint = "priorities";

        public PrioritiesClient(HttpClient httpClient) : base(httpClient, "priorities")
        {
        }

        public async Task<List<Bug>?> GetBugsWithPriority(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Bug>>($"api/{_endpoint}/{id}/bugs");
        }
    }
}
