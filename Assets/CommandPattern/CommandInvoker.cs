using TNRD;
using UnityEngine;

[CreateAssetMenu]
public class CommandInvoker : ScriptableObject
{
    [SerializeField] public SerializableInterface<ICommand> command1;
    [SerializeField] public SerializableInterface<ICommand> command2;
    [SerializeField] public SerializableInterface<ICommand> command3;
    [SerializeField] public SerializableInterface<ICommand> command4;
    [SerializeField] public SerializableInterface<ICommand> command5;

    public void DoCommand(ICommand command)
    {
        command.Execute();
    }
    public void UnDoCommand(ICommand command)
    {
        command.UnExecute();
    }
}
