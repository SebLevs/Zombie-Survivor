using System.Linq;

public class PassNumberHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "SomeKey";
    }

    public bool HandleError(string message)
    {
        return message.Count(x => x is >= '0' and <= '9') > 0;
    }
}
