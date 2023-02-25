using UnityEngine;

public class StateControllerZombie : EnemyStateController
{
    public ZombieChaseState ChaseState { get; private set; }
    public ZombieCombatState CombatState { get; private set; }
    public ZombieDeadState DeadState { get; private set; }

    public override EnemyState GetDefaultState()
    {
        return ChaseState;
    }

    public override void TransitionToDeadState()
    {
        OnTransitionState(DeadState);
    }

    protected override void InitStates()
    {
        ChaseState = new ZombieChaseState(this);
        CombatState = new ZombieCombatState(this);
        DeadState = new ZombieDeadState(this);
    }
}
