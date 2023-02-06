using System.Collections;
using System.Collections.Generic;
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
        Transform shootFrom = Entity_Player.Instance.muzzle;
        BulletBehavior bullet = WeaponManager.Instance.bulletPool.GetFromAvailable(shootFrom.position, Quaternion.identity);
        bullet.transform.up = Player_Controller.Instance.lookDirection;
        bullet.rb.velocity = Player_Controller.Instance.lookDirection * m_context.bulletSpeed;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        m_context.stateController.OnTransitionState(m_context.stateContainer.State_Move);
    }
}
