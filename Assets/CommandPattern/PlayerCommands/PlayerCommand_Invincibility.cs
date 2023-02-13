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
        Entity_Player.Instance.isinvincible = true;
    }

    public void UnExecute()
    {
        Entity_Player.Instance.isinvincible = false;
    }
}
