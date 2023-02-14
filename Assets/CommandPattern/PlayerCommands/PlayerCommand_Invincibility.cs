using System;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class PlayerCommand_Invincibility : ICommand
{

    public PlayerCommand_Invincibility()
    {
        
    }
    
    public void Execute()
    {
        if (!Entity_Player.Instance.isPermaInvincible)
        {
            Entity_Player.Instance.isPermaInvincible = true;
        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.isPermaInvincible)
        {
            Entity_Player.Instance.isPermaInvincible = false;
        }
    }
}
