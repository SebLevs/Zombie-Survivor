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
        m_context.rb.velocity = new Vector2(m_context.controller.moveDirection.x * m_context.movSpeed, m_context.controller.moveDirection.y * m_context.movSpeed);
        if(m_context.rb.velocity == Vector2.zero)
        {
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_Idle);
        }
        if(m_context.DesiredActions.Contains(PlayerActionsType.SHOOT)) 
        {
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_Shoot);
        }
        if(m_context.DesiredActions.Contains(PlayerActionsType.SPECIALSHOOT))
        {
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_SpecialShoot);
        }
        if (m_context.DesiredActions.Contains(PlayerActionsType.DODGE))
        {
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_Dodge);
        }
    }
}
