namespace BugTracker.UI.Services.Warnings
{
    public static class GeneralWarnings
    {
        public static string FillRequiredField(string fieldName) => $"{fieldName} is required.";

        public static string InvalidLoginData => "Invalid email/username or password.";

        public static string InvalidInput => "Invalid input.";

    }
}
