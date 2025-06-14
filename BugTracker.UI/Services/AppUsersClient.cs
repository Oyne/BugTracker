using System.Net.Http.Json;
using BugTracker.Shared.DTOs;

namespace BugTracker.UI.Services
{
    public class AppUsersClient
    {
        private static string _endpoint = "appusers";
        private readonly HttpClient _httpClient;

        public AppUsersClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<List<AppUserDTO>>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<AppUserDTO>>>($"api/{_endpoint}");
            return response ?? new ApiResponse<List<AppUserDTO>>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<AppUserDTO>> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<AppUserDTO>>($"api/{_endpoint}/{userId}");
            return response ?? new ApiResponse<AppUserDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<AppUserDTO>> GetUserByEmailAsync(string email)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<AppUserDTO>>($"api/{_endpoint}/by-email/{email}");
            return response ?? new ApiResponse<AppUserDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<AppUserDTO>> GetUserByUserNameAsync(string userName)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<AppUserDTO>>($"api/{_endpoint}/by-userName/{userName}");
            return response ?? new ApiResponse<AppUserDTO>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<int>> GetUserCountAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<int>>($"api/{_endpoint}/count");
            return response ?? new ApiResponse<int>
            {
                Success = false,
                Error = "No response or invalid JSON",
                StatusCode = 500
            };
        }

        public async Task<ApiResponse<AppUserDTO>> CreateUserAsync(AppUserRegisterDTO userRegisterDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{_endpoint}", userRegisterDTO);
            if (response.Content is null)
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Empty response",
                    StatusCode = (int)response.StatusCode
                };

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AppUserDTO>>();
            if (apiResponse != null)
                apiResponse.StatusCode = (int)response.StatusCode;

            return apiResponse ?? new ApiResponse<AppUserDTO>
            {
                Success = false,
                Error = "Failed to parse response",
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<ApiResponse<AppUserDTO>> Login(AppUserLoginDTO userLoginDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/{_endpoint}/login", userLoginDTO);
            if (response.Content is null)
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Empty response",
                    StatusCode = (int)response.StatusCode
                };

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AppUserDTO>>();
            if (apiResponse != null)
                apiResponse.StatusCode = (int)response.StatusCode;

            return apiResponse ?? new ApiResponse<AppUserDTO>
            {
                Success = false,
                Error = "Failed to parse response",
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<ApiResponse<AppUserDTO>> UpdateUserAsync(AppUserDTO userUpdateDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/{_endpoint}", userUpdateDTO);
            if (response.Content is null)
                return new ApiResponse<AppUserDTO>
                {
                    Success = false,
                    Error = "Empty response",
                    StatusCode = (int)response.StatusCode
                };

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<AppUserDTO>>();
            if (apiResponse != null)
                apiResponse.StatusCode = (int)response.StatusCode;

            return apiResponse ?? new ApiResponse<AppUserDTO>
            {
                Success = false,
                Error = "Failed to parse response",
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<bool> DeleteUserByIdAsync(int appUserId)
        {
            var response = await _httpClient.DeleteAsync($"api/{_endpoint}/{appUserId}");
            return response.IsSuccessStatusCode;
        }
    }
}