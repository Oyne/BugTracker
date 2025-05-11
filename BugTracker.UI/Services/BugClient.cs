using BugTracker.Shared.Models;

namespace BugTracker.UI.Services
{
    public class BugClient : BaseApiClient<Bug>
    {
        public BugClient(HttpClient httpClient) : base(httpClient, "bugs")
        {
        }
    }
}
