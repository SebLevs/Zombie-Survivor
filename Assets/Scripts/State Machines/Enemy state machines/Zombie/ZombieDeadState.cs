using UnityEngine;

public class ZombieDeadState : EnemyState
{
    public ZombieDeadState(StateControllerZombie controller) : base(controller)
    {
    }

    public override void HandleStateTransition()
    {
    }

    public override void OnEnter()
    {
        Debug.Log($"{m_controller.Context.name} DEAD ENTER");
        m_controller.Context.PathfinderUtility.DisablePathfinding();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
