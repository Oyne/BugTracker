using BugTracker.Shared.Models;

namespace BugTracker.UI.Services
{
    public class CategoriesClient : BaseApiClient<Category>
    {
        public CategoriesClient(HttpClient httpClient) : base(httpClient, "categories")
        {
        }
    }
}
