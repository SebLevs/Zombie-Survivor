using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    public PoolPattern<BulletBehavior> bulletPool;
    public PoolPattern<BoomrangBehavior> boomPool;

    protected override void OnAwake()
    {
        base.OnAwake();
        boomPool.InitDefaultQuantity();
        bulletPool.InitDefaultQuantity();
    }
}
