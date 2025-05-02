using BugTracker.Shared.Models;

namespace BugTracker.UI.Services
{
    public class StatusesClient : BaseApiClient<Status>
    {
        public StatusesClient(HttpClient httpClient) : base(httpClient, "statuses")
        {
        }
    }
}
