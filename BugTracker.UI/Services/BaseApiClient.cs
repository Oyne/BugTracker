using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class BaseApiClient<T>
    {
        protected readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public BaseApiClient(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<T>>($"api/{_endpoint}") ?? new();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T?>($"api/{_endpoint}/{id}");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }

                throw;
            }
        }

        public async Task<HttpResponseMessage> CreateAsync(T item)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{_endpoint}", item);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateAsync(T item)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/{_endpoint}", item);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/{_endpoint}/{id}");
            return response;
        }
    }
}
