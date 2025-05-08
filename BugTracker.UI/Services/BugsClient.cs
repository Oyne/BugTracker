using BugTracker.Shared.Models;

namespace BugTracker.UI.Services
{
    public class BugsClient : BaseApiClient<Bug>
    {
        public BugsClient(HttpClient httpClient) : base(httpClient, "bugs")
        {
        }
    }
}
