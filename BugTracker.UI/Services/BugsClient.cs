using BugTracker.Shared.DTOs;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class BugsClient
    {
        private static string _endpoint = "bugs";
        private readonly HttpClient _httpClient;

        public BugsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<BugDTO>>> GetAllBugsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<BugDTO>>>($"api/{_endpoint}");
            return response ?? new ApiResponse<List<BugDTO>>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<BugDTO>> GetBugByIdAsync(int bugId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<BugDTO>>($"api/{_endpoint}/{bugId}");
            return response ?? new ApiResponse<BugDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<BugDTO>> CreateBugAsync(BugCreateDTO bugCreateDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{_endpoint}", bugCreateDTO);
            if (response.Content is null)
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "Empty response",
                    StatusCode = (int)response.StatusCode
                };

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<BugDTO>>();
            if (apiResponse != null)
                apiResponse.StatusCode = (int)response.StatusCode;

            return apiResponse ?? new ApiResponse<BugDTO>
            {
                Success = false,
                Error = "Failed to parse response",
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<ApiResponse<BugDTO>> UpdateBugAsync(BugUpdateDTO bugUpdateDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/{_endpoint}", bugUpdateDTO);
            if (response.Content is null)
                return new ApiResponse<BugDTO>
                {
                    Success = false,
                    Error = "Empty response",
                    StatusCode = (int)response.StatusCode
                };

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<BugDTO>>();
            if (apiResponse != null)
                apiResponse.StatusCode = (int)response.StatusCode;

            return apiResponse ?? new ApiResponse<BugDTO>
            {
                Success = false,
                Error = "Failed to parse response",
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<bool> DeleteBugByIdAsync(int bugId)
        {
            var response = await _httpClient.DeleteAsync($"api/{_endpoint}/{bugId}");
            return response.IsSuccessStatusCode;
        }
    }
}
