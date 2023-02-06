using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    public PoolPattern<BulletBehavior> bulletPool;

    protected override void OnAwake()
    {
        base.OnAwake();
        bulletPool.InitDefaultQuantity();
    }
}
