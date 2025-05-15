using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class BugClient : BaseApiClient<Bug>
    {
        private static string _endpoint = "bugs";

        public BugClient(HttpClient httpClient) : base(httpClient, _endpoint)
        {
        }

        public async Task<Bug?> GetByTitleAsync(string title)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Bug>($"api/{_endpoint}/by-title/{title}");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }
    }
}
