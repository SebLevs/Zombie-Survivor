using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeZombie: EnemyType
{
    ZombieIdleState m_idleState;

    public override void ReturnToPool(Enemy key)
    {
        EnemyManager.Instance.Zombies.ReturnToAvailable(key);
    }
}

public class ZombieIdleState : State<Enemy>
{
    public ZombieIdleState(Enemy context) : base(context)
    {

    }

    public override void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleStateTransition()
    {
        throw new System.NotImplementedException();
    }
}
