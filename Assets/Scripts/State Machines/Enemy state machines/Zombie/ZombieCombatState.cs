using UnityEngine;

public class ZombieCombatState : EnemyState
{
    private SequentialTimer m_delayedAttackTimer;
    private readonly int _attackAnimHash;

    public ZombieCombatState(StateControllerZombie controller) : base(controller)
    {
        _attackAnimHash = Animator.StringToHash("attack");
    }

    public override bool IsTransitionValid() { return true; }

    public override void HandleStateTransition()
    {
    }

    public override void OnEnter()
    {
        if (m_delayedAttackTimer != null) { return; }
        //if (m_controller.IsInAnimationState(m_groundSmashSkill.Animation)) { return; }

        m_delayedAttackTimer = TimerManager.Instance.AddSequentialTimer(m_controller.GetReactionTimeInRange(0.5f), () =>
        {
            if (!m_controller) { return; } // Whenever the scene is unloaded and the timer lingers: do nothing
            m_controller.Context.Animator.SetTrigger(_attackAnimHash);
        });
    }

    public override void OnExit()
    {
        TimerManager.Instance.RemoveSequentialTimer(m_delayedAttackTimer);
        m_delayedAttackTimer = null;
        m_controller.StopAllCoroutines();
    }

    public override void OnUpdate()
    {
    }
}
