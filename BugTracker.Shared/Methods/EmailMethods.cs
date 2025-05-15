namespace BugTracker.Shared.Methods
{
    public static class EmailMethods
    {
        public static bool IsValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
