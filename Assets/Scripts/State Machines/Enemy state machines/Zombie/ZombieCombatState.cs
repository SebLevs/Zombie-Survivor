using System.Collections;
using UnityEngine;

public class ZombieCombatState : EnemyState
{
    private readonly int _attackAnimHash;

    public ZombieCombatState(StateControllerZombie controller) : base(controller)
    {
        _attackAnimHash = Animator.StringToHash("attack");
    }

    public override void HandleStateTransition()
    {
    }

    public override void OnEnter()
    {
        m_controller.StartCoroutine(DelayedAttack());
    }

    public override void OnExit()
    {
        m_controller.StopAllCoroutines();
    }

    public override void OnUpdate()
    {
    }

    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(m_controller.GetReactionTimeInRange(0.5f));
        m_controller.Context.PathfinderUtility.DisablePathfinding();
        m_controller.Context.Animator.SetTrigger(_attackAnimHash);
    }
}
