using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

[CreateAssetMenu]
public class CommandInvoker : ScriptableObject
{
    [Serializable]
    public class CommandWrapper
    {
        public CommandType type;
        public SerializableInterface<ICommand> command;
    }

    public List<CommandWrapper> wrappers;
    public Dictionary<CommandType, ICommand> commandDic = new();

    public void Init()
    {
        commandDic.Clear();
        foreach (CommandWrapper commandWrapper in wrappers)
        {
            commandDic.Add(commandWrapper.type, commandWrapper.command.Value);
        }
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