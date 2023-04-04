public class EmailUsedHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "btn options";
    }

    public bool HandleError(string message)
    {
        return false;
    }
}
