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
        //Debug.Log("Enter MoveState");
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        m_controller.Rb.velocity = new Vector2(m_controller.Controller.moveDirection.x * m_controller.MovSpeed, m_controller.Controller.moveDirection.y * m_controller.MovSpeed);
        if(m_controller.Rb.velocity == Vector2.zero)
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Idle);
        }
        if (Entity_Player.Instance.Health.IsDead)
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Dead);
        }
        if(m_controller.DesiredActions.Contains(PlayerActionsType.SHOOT)) 
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Shoot);
        }
        if(m_controller.DesiredActions.Contains(PlayerActionsType.SPECIALSHOOT))
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_SpecialShoot);
        }
        if (m_controller.DesiredActions.Contains(PlayerActionsType.DODGE))
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Dodge);
        }
    }
}
