using System.Collections.Generic;
using TNRD;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    [SerializeField] private List<SerializableInterface<IHandleLoginError>> loginErrorHandler;
    [SerializeField] private List<SerializableInterface<IHandleSignUpError>> signUpErrorHandler;

    public string TryLoginHandleError(string message)
    {
        for (int i = loginErrorHandler.Count; i >= 0; i--)
        {
            SerializableInterface<IHandleSignUpError> handler = signUpErrorHandler[i];
            if (handler.Value.HandleError(message))
            {
                return handler.Value.ErrorMessageKey();
            }
        }
        return "tmp valid login";
    }

    public string TrySignUpHandleError(string email, string password)
    {
        for (int i = signUpErrorHandler.Count; i >= 0; i--)
        {
            SerializableInterface<IHandleLoginError> handler = loginErrorHandler[i];
            if (handler.Value.HandleError(email, password))
            {
                return handler.Value.ErrorMessageKey();
            }
        }

        return "tmp valid signup";
    }
}
