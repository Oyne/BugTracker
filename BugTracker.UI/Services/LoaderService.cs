namespace BugTracker.UI.Services
{
    public class LoaderService
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                _isLoading = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        public void Show() => IsLoading = true;
        public void Hide() => IsLoading = false;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}

