using BugTracker.Shared.Models;

namespace BugTracker.UI.Services
{
    public class PrioritiesClient : BaseApiClient<Priority>
    {
        public PrioritiesClient(HttpClient httpClient) : base(httpClient, "priorities")
        {
        }
    }

}
