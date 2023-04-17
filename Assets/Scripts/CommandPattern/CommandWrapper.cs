using System;
using TNRD;

[Serializable]
public class CommandWrapper
{
    public CommandType type;
    public SerializableInterface<ICommand> command;
}