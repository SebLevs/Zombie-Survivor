using UnityEngine;

public class StateControllerZombie : EnemyStateController
{
    public ZombieIdleState IdleState { get; private set; }

    protected override EnemyState GetDefaultState()
    {
        return IdleState;
    }

    protected override void InitStates()
    {
        IdleState = new ZombieIdleState(this);
    }
}
