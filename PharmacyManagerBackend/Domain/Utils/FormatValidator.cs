using Exceptions;

namespace Domain.Utils;

public static class FormatValidator
{
    public static void CheckValidEmailFormat(string email)
    {
        if (String.IsNullOrEmpty(email))
        {
            throw new ValidationException("email can't be empty");
        }
        else if (!isValidEmailFormat(email))
        {
            throw new ValidationException("email bad format");
        }
    }

    private static bool isValidEmailFormat(string email)
    {
        // reference: https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}