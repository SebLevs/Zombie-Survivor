using System.Collections.Generic;
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

    [HideInInspector] public List<SerializableInterface<ICommand>> allCommands;
    [HideInInspector] public List<string> commandName; //Dictionary didnt work and i'm mad

    public void Init()
    {
        allCommands.Add(command1);
        commandName.Add("Invincible");
        allCommands.Add(command2);
        commandName.Add("AttackCooldown");
        allCommands.Add(command3);
        commandName.Add("Special AttackCooldown");
        allCommands.Add(command4);
        commandName.Add("Boom Distance");
        allCommands.Add(command5);
        commandName.Add("MoveSpeed");
    }
    public void DoCommand(ICommand command)
    {
        command.Execute();
        Entity_Player.Instance.RefreshPlayerStats();
    }
    public void UnDoCommand(ICommand command)
    {
        command.UnExecute();
        Entity_Player.Instance.RefreshPlayerStats();
    }
}
