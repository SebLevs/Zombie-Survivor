using UnityEngine;

public class ZombieChaseState : EnemyState
{
    public ZombieChaseState(StateControllerZombie controller) : base(controller)
    {
    }

    public override void HandleStateTransition()
    {
        if (m_controller.Context.PathfinderUtility.HasReachedEndOfPath)
        {
            m_controller.OnTransitionState((m_controller as StateControllerZombie).CombatState);
        }
    }

    public override void OnEnter()
    {
        Debug.Log($"{m_controller.Context.name} CHASE ENTER");
        m_controller.Context.PathfinderUtility.EnablePathfinding();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
