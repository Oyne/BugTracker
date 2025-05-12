using BugTracker.Shared.Models;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class AppUserClient : BaseApiClient<AppUser>
    {
        private string _endpoint = "appusers";

        public AppUserClient(HttpClient httpClient) : base(httpClient, "appusers")
        {
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AppUser>($"api/{_endpoint}/by-email/{email}");
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

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AppUser>($"api/{_endpoint}/by-username/{username}");
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
