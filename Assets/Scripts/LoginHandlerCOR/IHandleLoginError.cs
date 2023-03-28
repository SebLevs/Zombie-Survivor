public interface IHandleLoginError
{
    string ErrorMessageKey();
    bool HandleError(string email, string password);
}
