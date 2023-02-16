using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    [Header("Player pools")]
    public PoolPattern<BulletBehavior> bulletPool;
    public PoolPattern<BoomerangBehavior> boomPool;

    protected override void OnAwake()
    {
        base.OnAwake();
        boomPool.InitDefaultQuantity();
        bulletPool.InitDefaultQuantity();
    }
}
