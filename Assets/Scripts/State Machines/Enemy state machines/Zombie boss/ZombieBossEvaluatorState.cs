using System;
using UnityEngine;

[Serializable]
public class ZombieBossEvaluatorState : EnemyState
{
    public ZombieBossEvaluatorState(EnemyStateController controller) : base(controller)
    {
    }

    public override bool IsTransitionValid() { return true; }

    public override void HandleStateTransition() { }

    public override void OnEnter()
    {
        m_controller.ReturnToDefaultStatetimer.Reset();
        m_controller.Context.PathfinderUtility.ResetEndReachedDistance();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if ((m_controller as StateControllerZombieBoss).GroundSmashState.IsTransitionValid())
        {
            m_controller.OnTransitionState((m_controller as StateControllerZombieBoss).GroundSmashState);
        }
    }
}
