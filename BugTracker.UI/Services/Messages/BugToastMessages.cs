using BugTracker.UI.Enums;
using BugTracker.UI.Models;

namespace BugTracker.UI.Services.Messages
{
    public static class BugToastMessages
    {
        private static readonly string _entityName = "Bug";
        private static readonly string _parameter = "Title";

        public static ToastMessage BugCreated(string title = "")
        {
            return string.IsNullOrEmpty(title) ?
                GeneralToastMessages.Created(_entityName) :
                new ToastMessage(
                    message: $"{_entityName} \"{title}\" created.",
                    level: ToastLevel.Success);
        }

        public static ToastMessage BugCreationError =>
            GeneralToastMessages.CreationError(_entityName);

        public static ToastMessage BugWithTitleAlreadyExists(string title) =>
            GeneralToastMessages.EntityWithParameterAlreadyExists(
                entityName: _entityName,
                parameter: _parameter,
                value: title);

        public static ToastMessage BugUpdated =>
            GeneralToastMessages.Updated(_entityName);

        public static ToastMessage BugUpdateError =>
            GeneralToastMessages.UpdateError(_entityName);

        public static ToastMessage BugDeleted(string title = "")
        {
            return string.IsNullOrEmpty(title) ?
                GeneralToastMessages.Deleted(_entityName) :
                new ToastMessage(
                    message: $"{_entityName} \"{title}\" deleted.",
                    level: ToastLevel.Success);
        }

        public static ToastMessage BugDeletionError =>
            GeneralToastMessages.DeletionError(_entityName);

        public static ToastMessage BugNotFound =>
            GeneralToastMessages.NotFound(_entityName);
    }
}
