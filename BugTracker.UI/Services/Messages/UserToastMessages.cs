using BugTracker.UI.Enums;
using BugTracker.UI.Models;
using BugTracker.UI.Services.Warnings;

namespace BugTracker.UI.Services.Messages
{
    public class UserToastMessages
    {
        private static readonly string _entityName = "User";
        private static readonly string _emailParameter = "email";
        private static readonly string _userNameParameter = "username";

        public static ToastMessage UserRegistered(string userName) =>
                new ToastMessage(
                    message: $"{_entityName} \"{userName}\" registered.",
                    level: ToastLevel.Success);

        public static ToastMessage UserLoggedIn(string userName) =>
                new ToastMessage(
                    message: $"Welcome \"{userName}\".",
                    level: ToastLevel.Success);

        public static ToastMessage InvalidLoginData =>
       new ToastMessage(
           message: GeneralWarnings.InvalidLoginData,
           level: ToastLevel.Error);

        public static ToastMessage UserRegistrationError =>
            new ToastMessage(
                message: $"There was an error registering the {_entityName.ToLower()}.",
                level: ToastLevel.Error);

        public static ToastMessage UserWithEmailAlreadyExists(string email) =>
            GeneralToastMessages.EntityWithParameterAlreadyExists(
                entityName: _entityName,
                parameter: _emailParameter,
                value: email);

        public static ToastMessage UserWithUserNameAlreadyExists(string userName) =>
            GeneralToastMessages.EntityWithParameterAlreadyExists(
                entityName: _entityName,
                parameter: _userNameParameter,
                value: userName);

        public static ToastMessage UserUpdated =>
            GeneralToastMessages.Updated(_entityName);

        public static ToastMessage BugUpdateError =>
            GeneralToastMessages.UpdateError(_entityName);

        public static ToastMessage UserDeleted(string userName = "")
        {
            return string.IsNullOrEmpty(userName) ?
                GeneralToastMessages.Deleted(_entityName) :
                new ToastMessage(
                    message: $"{_entityName} \"{userName}\" deleted.",
                    level: ToastLevel.Success);
        }

        public static ToastMessage UserDeletionError =>
            GeneralToastMessages.DeletionError(_entityName);

        public static ToastMessage UserNotFound =>
            GeneralToastMessages.NotFound(_entityName);
    }
}
