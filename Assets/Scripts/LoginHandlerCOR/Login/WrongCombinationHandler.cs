public class WrongCombinationHandler : IHandleLoginError
{
    public string ErrorMessageKey()
    {
        return "Some Key";
    }

    public bool HandleError(string email, string password)
    {
        return false;
    }
}
