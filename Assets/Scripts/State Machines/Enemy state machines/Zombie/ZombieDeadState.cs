using UnityEngine;

public class ZombieDeadState : EnemyState
{
    public ZombieDeadState(StateControllerZombie controller) : base(controller)
    {
    }

    public override void HandleStateTransition()
    {
    }

    public override bool IsTransitionValid() { return m_controller.Context.Health.IsDead; }

    public override void OnEnter()
    {
        m_controller.Context.PathfinderUtility.DisablePathfinding();
        m_controller.Context.SetColliderEnable(false);

/*        int temp = Random.Range(0, 20);
        if (temp == 1)
        {
            EnemyManager.Instance.PermaGold.GetFromAvailable(m_controller.transform.position, Quaternion.identity);
        }*/
        //EnemyManager.Instance.PermaGold.GetFromAvailable(m_controller.transform.position, Quaternion.identity);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
