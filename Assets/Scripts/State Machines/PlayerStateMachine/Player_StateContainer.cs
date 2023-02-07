using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateContainer : StateContainer
{
    public Player_IdleState State_Idle { get; private set; }
    public Player_MoveState State_Move { get; private set; }
    public Player_ShootState State_Shoot { get; private set; }
    public Player_SpecialShootState State_SpecialShoot { get; private set; }
    public Player_DodgeState State_Dodge { get; private set; }

    public Player_StateContainer(Entity_Player context) 
    {
        State_Idle = new Player_IdleState(context);
        State_Move = new Player_MoveState(context);
        State_Shoot = new Player_ShootState(context);
        State_SpecialShoot = new Player_SpecialShootState(context);
        State_Dodge = new Player_DodgeState(context);
    }
}
