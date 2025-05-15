using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class StatusClient : BaseApiClient<Status>
    {
        private static string _endpoint = "statuses";

        public StatusClient(HttpClient httpClient) : base(httpClient, _endpoint)
        {
        }

        public async Task<List<Bug>?> GetBugsWithStatusAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Bug>>($"api/{_endpoint}/{id}/bugs");
        }
    }
}
