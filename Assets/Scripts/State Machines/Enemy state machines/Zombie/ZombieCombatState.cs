using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ZombieCombatState : EnemyState
{
    private const string _attackAnimName = "attack";
    private readonly float _attackOffsetMax = Time.deltaTime * 2;
    public ZombieCombatState(StateControllerZombie controller) : base(controller)
    {
    }

    public override void HandleStateTransition()
    {
    }

    public override void OnEnter()
    {
        Debug.Log($"{m_controller.Context.name} COMBAT ENTER");

        m_controller.StartCoroutine(DelayedAttack());
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }

    private IEnumerator DelayedAttack()
    {
        float attackOffset = Random.Range(0, _attackOffsetMax);
        yield return new WaitForSeconds(attackOffset);
        m_controller.Context.PathfinderUtility.DisablePathfinding();
        m_controller.Context.Animator.SetTrigger(_attackAnimName);
    }
}
