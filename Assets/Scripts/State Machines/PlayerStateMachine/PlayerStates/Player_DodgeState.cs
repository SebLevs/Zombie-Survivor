using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_DodgeState : State<Entity_Player>
{
    public Vector2 startLocation;
    public Vector2 targetLocation;
    public float moveDuration = 0.5f; //change for animation length
    private float moveStopWatch = 0;
    public bool isRolling = false;
    public Player_DodgeState(Entity_Player context) : base(context)
    {
        
    }

    public override void HandleStateTransition()
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Enter dodgeState");
        m_context.DesiredActions.ConsumeAllActions(PlayerActionsType.DODGE);
        if(m_context.canDodge)
        {
            Vector2 dodgeDirection = Player_Controller.Instance.currentPlayerLookDirection;
            startLocation = m_context.transform.position;
            targetLocation = new Vector2(startLocation.x + (dodgeDirection.x * m_context.dodgeDistance), startLocation.y + (dodgeDirection.y * m_context.dodgeDistance));
            isRolling = true;
            moveStopWatch = 0;
            m_context.rb.velocity = Vector2.zero;
            m_context.canDodge = false;
            m_context.dodgeDelay.Reset();
            m_context.dodgeDelay.StartTimer();
        }
        else
        {
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_Move);
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if(isRolling)
        {
            moveStopWatch += Time.deltaTime;
            m_context.transform.position = Vector2.Lerp(startLocation, targetLocation, m_context.dodgeCurve.Evaluate(moveStopWatch / moveDuration));
        }
        if(m_context.transform.position.x == targetLocation.x && m_context.transform.position.y == targetLocation.y)
        {
            isRolling= false;
            m_context.stateController.OnTransitionState(m_context.stateContainer.State_Move);
        }
    }
}
