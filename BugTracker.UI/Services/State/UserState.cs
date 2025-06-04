using BugTracker.Shared.DTOs;
using BugTracker.UI.Enums;

namespace BugTracker.UI.Services.State
{
    public class UserState
    {
        private const string UserKey = "loggedInUser";
        private readonly StorageService _storage;

        public AppUserDTO? CurrentUser { get; private set; }
        public bool IsAuthenticated => CurrentUser != null;

        public UserState(StorageService storage)
        {
            _storage = storage;
        }

        public async Task LoadUserAsync()
        {
            // First check sessionStorage
            CurrentUser = await _storage.GetItemAsync<AppUserDTO>(UserKey, StorageType.Session)
                          ?? await _storage.GetItemAsync<AppUserDTO>(UserKey, StorageType.Local);
        }

        public async Task SetUser(AppUserDTO appUser, bool rememberMe = false)
        {
            CurrentUser = appUser;
            var type = rememberMe ? StorageType.Local : StorageType.Session;
            await _storage.SetItemAsync(UserKey, appUser, type);
        }

        public async Task ClearUserAsync()
        {
            CurrentUser = null;
            await _storage.RemoveItemAsync(UserKey);
        }

        public async Task EnsureAuthenticatedAsync(NavigationService navigationService)
        {
            await LoadUserAsync();
            if (!IsAuthenticated)
            {
                navigationService.NavigateToLoginPage();
            }
        }

    }
}
