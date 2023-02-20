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
        m_controller.DesiredActions.ConsumeAllActions(PlayerActionsType.DODGE);
        if(m_controller.canDodge)
        {
            Vector2 dodgeDirection = Player_Controller.Instance.normalizedLookDirection;
            startLocation = m_controller.transform.position;
            targetLocation = new Vector2(startLocation.x + (dodgeDirection.x * m_controller.dodgeDistance), startLocation.y + (dodgeDirection.y * m_controller.dodgeDistance));
            isRolling = true;
            moveStopWatch = 0;
            m_controller.Rb.velocity = Vector2.zero;
            m_controller.canDodge = false;
            m_controller.dodgeDelay.Reset();
            m_controller.dodgeDelay.StartTimer();
            m_controller.gameObject.layer = 10;
        }
        else
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Move);
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
            m_controller.transform.position = Vector2.Lerp(startLocation, targetLocation, m_controller.dodgeCurve.Evaluate(moveStopWatch / moveDuration));
        }
        if(m_controller.transform.position.x == targetLocation.x && m_controller.transform.position.y == targetLocation.y)
        {
            isRolling= false;
            m_controller.gameObject.layer = 3;
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Move);
        }
    }
}
