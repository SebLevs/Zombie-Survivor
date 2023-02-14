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
