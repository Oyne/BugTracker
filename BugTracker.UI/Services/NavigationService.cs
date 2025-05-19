using Microsoft.AspNetCore.Components;

namespace BugTracker.UI.Services
{
    public class NavigationService
    {
        private readonly NavigationManager _navigationManager;

        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void NavigateToHomePage()
        {
            _navigationManager.NavigateTo("/");
        }

        public void NavigateToLoginPage()
        {
            _navigationManager.NavigateTo("/login");
        }

        public void NavigateToCreateBugPage()
        {
            _navigationManager.NavigateTo("/create-bug");
        }

        public void NavigateToBugPage(int bugId)
        {
            _navigationManager.NavigateTo($"/bug/{bugId}");
        }

        public void NavigateToUsersPage()
        {
            _navigationManager.NavigateTo("/users");
        }
    }
}
