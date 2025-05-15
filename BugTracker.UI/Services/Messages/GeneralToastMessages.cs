using BugTracker.UI.Enums;
using BugTracker.UI.Models;

namespace BugTracker.UI.Services.Messages
{
    public static class GeneralToastMessages
    {
        public static ToastMessage FillRequiredFields =>
            new ToastMessage(
                message: $"Please fill all required fields.",
                level: ToastLevel.Warning);

        public static ToastMessage SomethingWentWrong =>
           new ToastMessage(
               message: $"Something went wrong.",
               level: ToastLevel.Error);

        public static ToastMessage NotFound(string entityName) =>
            new ToastMessage(
                message: $"{entityName} not found.",
                level: ToastLevel.Error);

        public static ToastMessage Created(string entityName) =>
            new ToastMessage(
                message: $"{entityName} created.",
                level: ToastLevel.Success);

        public static ToastMessage CreationError(string entityName) =>
            new ToastMessage(
                message: $"There was an error creating the {entityName}.",
                level: ToastLevel.Error);

        public static ToastMessage Updated(string entityName) =>
            new ToastMessage(
                message: $"{entityName} updated.",
                level: ToastLevel.Success);

        public static ToastMessage UpdateError(string entityName) =>
            new ToastMessage(
                message: $"There was an error updating the {entityName}.",
                level: ToastLevel.Error);

        public static ToastMessage Deleted(string entityName) =>
            new ToastMessage(
                message: $"{entityName} deleted.",
                level: ToastLevel.Success);

        public static ToastMessage DeletionError(string entityName) =>
            new ToastMessage(
                message: $"There was an error deleting the {entityName}.",
                level: ToastLevel.Error);

        public static ToastMessage EntityWithParameterAlreadyExists(string entityName, string parameter, string value) =>
            new ToastMessage(
                message: $"{entityName} with {parameter} \"{value}\" already exists.",
                level: ToastLevel.Error);
    }
}
