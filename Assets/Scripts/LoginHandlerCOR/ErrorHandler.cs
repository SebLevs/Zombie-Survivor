using System.Collections.Generic;
using TNRD;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    [SerializeField] private List<SerializableInterface<IHandleLoginError>> loginErrorHandler;
    [SerializeField] private List<SerializableInterface<IHandleSignUpError>> signUpUserErrorHandler;
    [SerializeField] private List<SerializableInterface<IHandleSignUpError>> signUpPassErrorHandler;
    public string errorKey = "";

    public bool TryLoginHandleError(string email, string password)
    {
        foreach (var handler in loginErrorHandler)
        {
            if (handler.Value.HandleError(email, password))
            {
                errorKey = handler.Value.ErrorMessageKey();
                return true;
            }
        }
        return false;
    }

    public bool TrySignUpHandleError(string email, string password)
    {
        foreach (var handler in signUpUserErrorHandler)
        {
            if (handler.Value.HandleError(email))
            {
                errorKey = handler.Value.ErrorMessageKey();
                return true;
            }
        }
        foreach (var handler in signUpPassErrorHandler)
        {
            if (handler.Value.HandleError(password))
            {
                errorKey = handler.Value.ErrorMessageKey();
                return true;
            }
        }
        
        return false;
    }
}