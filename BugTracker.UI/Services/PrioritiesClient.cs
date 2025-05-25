using BugTracker.Shared.DTOs;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class PrioritiesClient
    {
        private static string _endpoint = "priorities";
        private readonly HttpClient _httpClient;

        public PrioritiesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<PriorityDTO>>> GetAllPrioritiesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<PriorityDTO>>>($"api/{_endpoint}");
            return response ?? new ApiResponse<List<PriorityDTO>>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<PriorityDTO>> GetPriorityByIdAsync(int priorityId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<PriorityDTO>>($"api/{_endpoint}/{priorityId}");
            return response ?? new ApiResponse<PriorityDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }
    }
}
