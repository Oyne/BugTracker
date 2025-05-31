using BugTracker.Shared.DTOs;
using System.Net.Http.Json;

namespace BugTracker.UI.Services
{
    public class RolesClient
    {
        private const string _endpoint = "roles";
        private readonly HttpClient _httpClient;

        public RolesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<RoleDTO>>> GetAllRolesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<RoleDTO>>>($"api/{_endpoint}");
            return response ?? new ApiResponse<List<RoleDTO>>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<RoleDTO>> GetRoleByIdAsync(int roleId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<RoleDTO>>($"api/{_endpoint}/{roleId}");
            return response ?? new ApiResponse<RoleDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }
    }
}