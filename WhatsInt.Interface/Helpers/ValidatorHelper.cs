namespace WhatsInt.Interface.Helpers
{
    internal static class ValidatorHelper
    {
        internal static bool ValidateEmail(this string email)
        {
            return email != null && email.Contains("@") && email.Contains(".");
        }
        
        internal static bool ValidatePassword(this string password, string confirmation)
        {
            return string.IsNullOrEmpty(confirmation) ? true : password == confirmation;
        }
    }
}
