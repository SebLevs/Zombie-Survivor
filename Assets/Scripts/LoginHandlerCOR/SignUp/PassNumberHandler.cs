using System;
using System.Linq;

public class PassNumberHandler : IHandleSignUpError
{
    public string ErrorMessageKey()
    {
        return "error password number";
    }

    public bool HandleError(string message)
    {
        int count = 0;
        foreach (var c in message)
        {
            if (char.IsNumber(c)) count++;
        }

        return count == 0;
    }
}
