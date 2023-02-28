using System;
using System.Collections.Generic;
using System.Linq;
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

    public AudioElement powerUpSound;

    public void Init()
    {
        commandDic.Clear();
        foreach (CommandWrapper commandWrapper in wrappers)
        {
            commandDic.Add(commandWrapper.type, commandWrapper.command.Value);
        }

        powerUpSound.AudioSource = UIManager.Instance.AudioSource;
    }

    public void DoCommand(ICommand command)
    {
        command.Execute();
        powerUpSound.PlayRandom();
        Entity_Player.Instance.RefreshPlayerStats();
    }

    public void UnDoCommand(ICommand command)
    {
        command.UnExecute();
        powerUpSound.PlayRandom();
        Entity_Player.Instance.RefreshPlayerStats();
    }
}