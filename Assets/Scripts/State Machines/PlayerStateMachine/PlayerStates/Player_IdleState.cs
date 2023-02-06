using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : State<Entity_Player>
{
    public Player_IdleState(Entity_Player context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Idle");
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if(Player_Controller.Instance.moveDirection != Vector2.zero) 
        {
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_Move);
        }
    }
}
