using UnityEngine;

public class ZombieIdleState : EnemyState
{
    public ZombieIdleState(StateControllerZombie controller) : base(controller)
    {
    }

    public override bool IsTransitionValid() { return true; }

    public override void OnEnter()
    {
        Debug.Log($"{m_controller.Context.name} IDLE ENTER");
        m_controller.Context.PathfinderUtility.DisablePathfinding();
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }

    public override void HandleStateTransition()
    {
    }
}
