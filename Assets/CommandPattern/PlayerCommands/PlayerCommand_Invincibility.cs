using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCommand_Invincibility : ICommand
{
    Entity_Player player;
    public PlayerCommand_Invincibility(Entity_Player player)
    {
        this.player = player;
    }
    public void Execute()
    {
        player.isinvincible = true;
    }

    public void UnExecute()
    {
        player.isinvincible = false;
    }
}
