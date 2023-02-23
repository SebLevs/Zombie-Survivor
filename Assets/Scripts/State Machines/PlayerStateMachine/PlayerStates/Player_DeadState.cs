using UnityEngine;

public class Player_DeadState : State<Entity_Player>
{
    public Player_DeadState(Entity_Player context) : base(context)
    {
    }

    public override void OnEnter()
    {
        //Debug.Log("Enter Dead State");
        Entity_Player.Instance.Rb.velocity = Vector2.zero;
        UIManager.Instance.DeathTransition();
        Entity_Player.Instance.GetComponent<Animator>().enabled = false;
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        Entity_Player.Instance.GetComponent<Animator>().enabled = true;
    }

    public override void HandleStateTransition()
    {
        if (!m_controller.Health.IsDead)
        {
            m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Idle);
        }
    }
}
