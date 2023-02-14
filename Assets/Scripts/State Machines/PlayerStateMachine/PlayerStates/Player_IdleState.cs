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
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_Move);
        }
        if (m_context.DesiredActions.Contains(PlayerActionsType.SHOOT))
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_Shoot);
        }
        if (m_context.DesiredActions.Contains(PlayerActionsType.SPECIALSHOOT))
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_SpecialShoot);
        }
        if (m_context.DesiredActions.Contains(PlayerActionsType.DODGE))
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_Dodge);
        }
    }
}
