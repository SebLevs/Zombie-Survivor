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

    public List<CommandWrapper> promptOnly;
    public List<CommandWrapper> promptAndChest;
    
    public readonly Dictionary<CommandType, ICommand> CommandPromptDic = new();
    public readonly Dictionary<CommandType, ICommand> ChestPowerUpDic = new();

    public AudioElement powerUpSound;
    
    
    public void Init()
    {
        CommandPromptDic.Clear();
        ChestPowerUpDic.Clear();
        foreach (CommandWrapper commandWrapper in promptOnly)
        {
            CommandPromptDic.Add(commandWrapper.type, commandWrapper.command.Value);
        }
        foreach (CommandWrapper commandWrapper in promptAndChest)
        {
            CommandPromptDic.Add(commandWrapper.type, commandWrapper.command.Value);
            ChestPowerUpDic.Add(commandWrapper.type, commandWrapper.command.Value);
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