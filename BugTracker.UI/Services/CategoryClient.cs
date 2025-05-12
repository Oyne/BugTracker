using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class CategoryClient : BaseApiClient<Category>
    {
        private string _endpoint = "categories";

        public CategoryClient(HttpClient httpClient) : base(httpClient, "categories")
        {
        }

        public async Task<List<Bug>?> GetBugsWithCategoryAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Bug>>($"api/{_endpoint}/{id}/bugs");
        }
    }
}
