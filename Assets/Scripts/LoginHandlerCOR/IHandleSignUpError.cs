public interface IHandleSignUpError
{
    string ErrorMessageKey();
    bool HandleError(string message);
}
