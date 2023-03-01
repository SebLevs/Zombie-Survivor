using System;
using UnityEngine;

[Serializable]
public class ZombieBossGroundSmashState : EnemyState
{
    [SerializeField] private EnemySkill m_groundSmashSkill;
    private SequentialTimer m_delayedAttackTimer;

    public ZombieBossGroundSmashState(EnemyStateController controller, EnemySkill groundSmashSkill) : base(controller)
    {
        m_groundSmashSkill = groundSmashSkill;
    }

    public override bool IsTransitionValid()
    {
        float distance = LinearAlgebraUtilities.GetDistance2D(m_controller.Context.PathfinderUtility.GetTarget().position, m_controller.Context.transform.position);
        if (distance <= m_groundSmashSkill.ExecuteAtDistance * 1.5f)
        {
            return true;
        }

        return false;
    }

    public override void HandleStateTransition() { }

    public override void OnEnter()
    {
        // offset added to never be right at the perimeter of an attack
        //m_controller.Context.PathfinderUtility.SetEndReachedDistance(m_groundSmashSkill.ExecuteAtDistance * 0.95f);
    }

    public override void OnExit()
    {
        m_delayedAttackTimer = null;
    }

    public override void OnUpdate()
    {
        if (m_delayedAttackTimer != null) { return; }
        if (m_controller.IsInAnimationState(m_groundSmashSkill.Animation)) { return; }

        if (m_groundSmashSkill.CanExecute(m_controller.Context.PathfinderUtility.GetTarget(), m_controller.Context.transform))
        {
            m_delayedAttackTimer = TimerManager.Instance.AddSequentialTimer(m_controller.GetReactionTimeInRange(0.5f), () =>
            {
                if (!m_groundSmashSkill) { return; }
                m_groundSmashSkill.SetAnimatorTrigger(m_controller.Context.Animator);
            });
        }
        else
        {
            m_controller.ReturnToDefaultStatetimer.OnUpdateTime();
        }
    }
}
