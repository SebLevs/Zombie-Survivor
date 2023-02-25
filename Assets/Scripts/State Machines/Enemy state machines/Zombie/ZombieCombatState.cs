using System.Collections;
using UnityEngine;

public class ZombieCombatState : EnemyState
{
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
        m_controller.StartCoroutine(m_controller.DelayedAnimatorTrigger(_attackAnimHash, 0.5f));
    }

    public override void OnExit()
    {
        m_controller.StopAllCoroutines();
    }

    public override void OnUpdate()
    {
    }
}
