using BugTracker.Shared.DTOs;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class CategoriesClient
    {
        private static string _endpoint = "categories";
        private readonly HttpClient _httpClient;

        public CategoriesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<CategoryDTO>>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CategoryDTO>>>($"api/{_endpoint}");
            return response ?? new ApiResponse<List<CategoryDTO>>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<CategoryDTO>> GetCategoryByIdAsync(int categoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<CategoryDTO>>($"api/{_endpoint}/{categoryId}");
            return response ?? new ApiResponse<CategoryDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }
    }
}