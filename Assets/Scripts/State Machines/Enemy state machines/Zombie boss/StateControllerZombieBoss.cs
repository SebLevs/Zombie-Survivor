using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControllerZombieBoss : EnemyStateController
{
    public ZombieBossChaseState ChaseState { get; private set; }
    public ZombieBossCombatState CombatState { get; private set; }
    public ZombieBossDeadState DeadState { get; private set; }

    public override EnemyState GetDefaultState()
    {
        return ChaseState;
    }

    protected override void InitStates()
    {
        ChaseState = new ZombieBossChaseState(this);
        CombatState = new ZombieBossCombatState(this);
        DeadState = new ZombieBossDeadState(this);
    }

    /*
        if (m_controller.Context.PathfinderUtility.HasReachedEndOfPath)
        {
            m_controller.OnTransitionState((m_controller as StateControllerZombie).CombatState);
        }
     */
}
