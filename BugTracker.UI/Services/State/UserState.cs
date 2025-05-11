using BugTracker.Shared.Models;
using BugTracker.UI.Enums;

namespace BugTracker.UI.Services.State
{
    public class UserState
    {
        private const string UserKey = "loggedInUser";
        private readonly StorageService _storage;

        public AppUser? CurrentUser { get; private set; }
        public bool IsAuthenticated => CurrentUser != null;

        public UserState(StorageService storage)
        {
            _storage = storage;
        }

        public async Task LoadUserAsync()
        {
            // First check sessionStorage
            CurrentUser = await _storage.GetItemAsync<AppUser>(UserKey, StorageType.Session)
                          ?? await _storage.GetItemAsync<AppUser>(UserKey, StorageType.Local);
        }

        public async Task SetUser(AppUser user, bool rememberMe = false)
        {
            CurrentUser = user;
            var type = rememberMe ? StorageType.Local : StorageType.Session;
            await _storage.SetItemAsync(UserKey, user, type);
        }

        public async Task ClearUserAsync()
        {
            CurrentUser = null;
            await _storage.RemoveItemAsync(UserKey);
        }
    }
}
