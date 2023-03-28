public class PassLengthHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "error password length";
    }

    public bool HandleError(string message)
    {
        return message.Length <= 5;
    }
}
