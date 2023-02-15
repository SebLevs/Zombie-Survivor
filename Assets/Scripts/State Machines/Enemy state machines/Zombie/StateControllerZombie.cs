using UnityEngine;

public class StateControllerZombie : EnemyStateController
{
    public ZombieChaseState ChaseState { get; private set; }
    public ZombieCombatState CombatState { get; private set; }
    public ZombieDeadState DeadState { get; private set; }

    protected override EnemyState GetDefaultState()
    {
        return ChaseState;
    }

    protected override void InitStates()
    {
        ChaseState = new ZombieChaseState(this);
        CombatState = new ZombieCombatState(this);
        DeadState = new ZombieDeadState(this);
    }
}
