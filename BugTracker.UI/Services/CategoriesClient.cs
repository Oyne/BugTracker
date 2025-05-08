using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class CategoriesClient : BaseApiClient<Category>
    {
        private string _endpoint = "categories";

        public CategoriesClient(HttpClient httpClient) : base(httpClient, "categories")
        {
        }

        public async Task<List<Bug>?> GetBugsWithCategory(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Bug>>($"api/{_endpoint}/{id}/bugs");
        }
    }
}
