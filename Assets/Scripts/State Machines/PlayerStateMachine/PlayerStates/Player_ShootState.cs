using UnityEngine;

public class Player_ShootState : State<Entity_Player>
{
    public Player_ShootState(Entity_Player context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
        
    }

    public override void OnEnter()
    {
        Debug.Log("Enter ShootState");

        m_context.DesiredActions.ConsumeAllActions(PlayerActionsType.SHOOT);
        if(m_context.canAttack)
        {
            Transform shootFrom = Entity_Player.Instance.muzzle;
            BulletBehavior bullet = WeaponManager.Instance.bulletPool.GetFromAvailable(shootFrom.position, Quaternion.identity);
            bullet.ShootBullet(Player_Controller.Instance.normalizedLookDirection, m_context.BulletSpeed);
            m_context.canAttack = false;
            m_context.attackDelay.Reset();
            m_context.attackDelay.StartTimer();
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        m_context.StateController.OnTransitionState(m_context.StateContainer.State_Move);
    }
}
