public class EmailUsedHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "Some Key";
    }

    public bool HandleError(string message)
    {
        return false;
    }
}
