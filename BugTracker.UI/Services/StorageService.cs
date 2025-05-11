using BugTracker.UI.Enums;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BugTracker.UI.Services
{
    public class StorageService
    {
        private readonly IJSRuntime _js;

        public StorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetItemAsync<T>(string key, T value, StorageType storageType = StorageType.Session)
        {
            var json = JsonSerializer.Serialize(value);
            var method = storageType == StorageType.Local ? "localStorage.setItem" : "sessionStorage.setItem";
            await _js.InvokeVoidAsync(method, key, json);
        }

        public async Task<T?> GetItemAsync<T>(string key, StorageType storageType = StorageType.Session)
        {
            var method = storageType == StorageType.Local ? "localStorage.getItem" : "sessionStorage.getItem";
            var json = await _js.InvokeAsync<string>(method, key);
            return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
            await _js.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }
}
