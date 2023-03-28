public class WrongCombinationHandler : IHandleLoginError
{
    public string ErrorMessageKey()
    {
        return "error wrong combination";
    }

    public bool HandleError(string email, string password)
    {
        return false;
    }
}
