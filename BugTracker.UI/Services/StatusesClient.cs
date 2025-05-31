using BugTracker.Shared.DTOs;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class StatusesClient
    {
        private static string _endpoint = "statuses";
        private readonly HttpClient _httpClient;

        public StatusesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<StatusDTO>>> GetAllStatusesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<StatusDTO>>>($"api/{_endpoint}");
            return response ?? new ApiResponse<List<StatusDTO>>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<StatusDTO>> GetStatusByIdAsync(int statusId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<StatusDTO>>($"api/{_endpoint}/{statusId}");
            return response ?? new ApiResponse<StatusDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }
    }
}
