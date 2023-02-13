using TNRD;
using UnityEngine;
using Object = System.Object;

[CreateAssetMenu]
public class CommandInvoker : ScriptableObject
{
    [SerializeField] public SerializableInterface<ICommand> command1;
    [SerializeField] public SerializableInterface<ICommand> command2;

    public void DoCommand(ICommand command)
    {
        command.Execute();
    }
    public void UnDoCommand(ICommand command)
    {
        command.UnExecute();
    }
}
