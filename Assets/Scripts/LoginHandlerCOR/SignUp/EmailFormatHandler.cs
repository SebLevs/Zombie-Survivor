public class EmailFormatHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "Some key";
    }

    public bool HandleError(string message)
    {
        var trimmedEmail = message.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return false;
        }
        try {
            var addr = new System.Net.Mail.MailAddress(message);
            return addr.Address != trimmedEmail;
        }
        catch
        {
            return true;
        }
    }
}
