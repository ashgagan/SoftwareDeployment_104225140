using System.Text.RegularExpressions;

namespace ContactValidationLibrary
{
    public class AdvancedValidator
    {
        public static bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool ValidatePhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\+?[\d\s\-\(\)]{7,}$");
        }

        public static string GetValidationMessage(string name, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(name)) return "Name is required";
            if (!ValidateEmail(email)) return "Invalid email format";
            if (!ValidatePhone(phone)) return "Invalid phone format";
            return "Valid";
        }
    }
}