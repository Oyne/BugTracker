using BugTracker.UI.Models;

namespace BugTracker.UI.Services
{
    public class ToastService
    {
        public event Action? OnToastsUpdated;

        public List<ToastMessage> Toasts { get; } = new();

        public void ShowToast(ToastMessage toastMessage)
        {
            if (Toasts.Count >= 5)
            {
                Toasts.RemoveAt(0);
            }
            Toasts.Add(toastMessage);
            OnToastsUpdated?.Invoke();
        }

        public void RemoveToast(Guid id)
        {
            var toast = Toasts.FirstOrDefault(t => t.Id == id);
            if (toast != null)
            {
                Toasts.Remove(toast);
                OnToastsUpdated?.Invoke();
            }
        }
    }
}
