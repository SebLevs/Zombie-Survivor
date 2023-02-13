using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Enter SpecialShootState");
        m_context.DesiredActions.ConsumeAllActions(PlayerActionsType.SPECIALSHOOT);
        if(m_context.canSpecialAttack)
        {
            Transform shootFrom = Entity_Player.Instance.muzzle;
            BoomerangBehavior boomerang = WeaponManager.Instance.boomPool.GetFromAvailable(shootFrom.position, Quaternion.identity);
            boomerang.ShootBoom();
            //boomerang.strategy.Execute(boomerang);
            m_context.canSpecialAttack = false;
            m_context.specialAttackDelay.Reset();
            m_context.specialAttackDelay.StartTimer();
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
