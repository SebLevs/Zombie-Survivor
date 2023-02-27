using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand_FullHeal : ICommand
{
    public PlayerCommand_FullHeal()
    {
    }

    public void Execute()
    {
        if (Entity_Player.Instance.Health.CurrentHP <= Entity_Player.Instance.Health.MaxHP)
        {
            Entity_Player.Instance.Health.SetCurrentHP(Entity_Player.Instance.Health.MaxHP);
            Entity_Player.Instance.RefreshHealthBar();
        }
    }
        

    public void UnExecute()
    {
        Entity_Player.Instance.Health.OnInstantDeath();
        Entity_Player.Instance.RefreshHealthBar();
    }
}
