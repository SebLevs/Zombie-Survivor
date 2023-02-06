using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShootState : State<Entity_Player>
{
    public Player_ShootState(Entity_Player context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Enter ShootState");
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
