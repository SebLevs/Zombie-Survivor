using UnityEngine;

public class ZombieDeadState : EnemyState
{
    public ZombieDeadState(StateControllerZombie controller) : base(controller)
    {
    }

    public override void HandleStateTransition()
    {
    }

    public override bool IsTransitionValid() { return m_controller.Context.Health.IsDead; }

    public override void OnEnter()
    {
        m_controller.Context.PathfinderUtility.DisablePathfinding();
        m_controller.Context.SetColliderEnable(false);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
