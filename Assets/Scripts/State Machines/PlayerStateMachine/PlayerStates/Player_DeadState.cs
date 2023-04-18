using UnityEngine;

public class Player_DeadState : State<Entity_Player>
{
    public Player_DeadState(Entity_Player context) : base(context)
    {
    }

    public override void OnEnter()
    {
        UIManager.Instance.TransitionToDeathScreenView();
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        m_controller.UnFreeze();
    }


    public override void HandleStateTransition()
    {
    }
}
