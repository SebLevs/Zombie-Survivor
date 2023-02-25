using UnityEngine;

public class StateControllerZombieBoss : EnemyStateController
{
    [field:SerializeField] public ZombieBossEvaluatorState ChaseState { get; private set; }
    [field: SerializeField] public ZombieBossGroundSmashState GroundSmashState { get; private set; }
    // TODO: Refactor into a way to be used inside of non-monobehaviour OR change enemy states into monobehaviours OR Make states into scriptable objects and make skill into serializable
    [SerializeField] private EnemySkill _groundSmashSkill; 
    [field: SerializeField] public ZombieBossDeadState DeadState { get; private set; }

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
        ChaseState = new ZombieBossEvaluatorState(this);
        GroundSmashState = new ZombieBossGroundSmashState(this, _groundSmashSkill);
        DeadState = new ZombieBossDeadState(this);
    }

    /*
        if (m_controller.Context.PathfinderUtility.HasReachedEndOfPath)
        {
            m_controller.OnTransitionState((m_controller as StateControllerZombie).GroundSmashState);
        }
     */
}
