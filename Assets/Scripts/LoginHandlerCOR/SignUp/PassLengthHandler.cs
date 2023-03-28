public class PassLengthHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "Some key";
    }

    public bool HandleError(string message)
    {
        return message.Length <= 5;
    }
}
