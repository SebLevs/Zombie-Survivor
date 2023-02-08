using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandInvoker
{
    [SerializeField] public ICommand command1;
    [SerializeField] public ICommand command2;

    public CommandInvoker(ICommand command1, ICommand command2)
    {
        this.command1 = command1;
        this.command2 = command2;
    }

    public void DoCommand(ICommand command)
    {
        command.Execute();
    }
    public void UnDoCommand(ICommand command)
    {
        command.UnExecute();
    }
}
