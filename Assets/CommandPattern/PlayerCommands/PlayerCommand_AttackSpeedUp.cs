using System;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class PlayerCommand_AttackSpeedUp : ICommand
{
    //[SerializeField] private Entity_Player player;

    public PlayerCommand_AttackSpeedUp()
    {
        
    }
    public void Execute()
    {
        if(Entity_Player.Instance.attackSpeed >= 0.05)
        {
            Entity_Player.Instance.attackSpeed -= 0.1f;
        }

    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.attackSpeed >= 2f)
        {
            Entity_Player.Instance.attackSpeed += 0.1f;
        }
    }
}
