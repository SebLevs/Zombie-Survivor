using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveState : State<Entity_Player>
{
    public Player_MoveState(Entity_Player context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Enter MoveState");
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        m_context.Rb.velocity = new Vector2(m_context.Controller.moveDirection.x * m_context.MovSpeed, m_context.Controller.moveDirection.y * m_context.MovSpeed);
        if(m_context.Rb.velocity == Vector2.zero)
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_Idle);
        }
        if(m_context.DesiredActions.Contains(PlayerActionsType.SHOOT)) 
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_Shoot);
        }
        if(m_context.DesiredActions.Contains(PlayerActionsType.SPECIALSHOOT))
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_SpecialShoot);
        }
        if (m_context.DesiredActions.Contains(PlayerActionsType.DODGE))
        {
            m_context.StateController.OnTransitionState(m_context.StateContainer.State_Dodge);
        }
    }
}
