using BugTracker.UI.Enums;

namespace BugTracker.UI.Models
{
    public class ToastMessage
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Message { get; }
        public ToastLevel Level { get; }

        public ToastMessage(string message, ToastLevel level)
        {
            Message = message;
            Level = level;
        }
    }
}
