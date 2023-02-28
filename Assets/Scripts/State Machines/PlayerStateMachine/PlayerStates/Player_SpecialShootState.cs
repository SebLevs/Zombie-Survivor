using UnityEngine;

public class Player_SpecialShootState : State<Entity_Player>
{
    public Player_SpecialShootState(Entity_Player context) : base(context)
    {
    }

    public override void HandleStateTransition()
    {
        
    }

    public override void OnEnter()
    {
        m_controller.DesiredActions.ConsumeAllActions(PlayerActionsType.SPECIALSHOOT);
        if(m_controller.canSpecialAttack)
        {
            Transform shootFrom = Entity_Player.Instance.muzzle;
            BoomerangBehavior boomerang = WeaponManager.Instance.boomPool.GetFromAvailable(shootFrom.position, Quaternion.identity);
            boomerang.ShootBoom();
            m_controller.canSpecialAttack = false;
            m_controller.specialAttackDelay.Reset();
            m_controller.specialAttackDelay.StartTimer();
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        m_controller.StateController.OnTransitionState(m_controller.StateContainer.State_Move);
    }
}
