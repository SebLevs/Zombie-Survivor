using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCommand_AttackSpeedUp : ICommand
{
    Entity_Player player;
    public PlayerCommand_AttackSpeedUp(Entity_Player player)
    {
        this.player = player;
    }
    public void Execute()
    {
        if(player.attackSpeed >= 0.2)
        {
            player.attackSpeed -= 0.1f;
        }
    }

    public void UnExecute()
    {
        if (player.attackSpeed >= 2f)
        {
            player.attackSpeed += 0.1f;
        }
    }
}
