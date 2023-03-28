public class EmailUsedHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "error email already in use";
    }

    public bool HandleError(string message)
    {
        return false;
    }
}
