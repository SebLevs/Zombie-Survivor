using UnityEngine;

public class Player_DeadState : State<Entity_Player>
{
    public Player_DeadState(Entity_Player context) : base(context)
    {
    }

    public override void OnEnter()
    {
        //Entity_Player.Instance.Freeze();
        UIManager.Instance.TransitionToDeathScreenView();
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        m_controller.UnFreeze();
        //Entity_Player.Instance.GetComponent<Animator>().enabled = true;
    }

    public override void HandleStateTransition()
    {
    }
}
