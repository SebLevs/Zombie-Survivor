using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    [Header("Player pools")]
    public PoolPattern<BulletBehavior> bulletPool;
    public PoolPattern<BoomerangBehavior> boomPool;

    [Header("Enemy pools")]
    public PoolPattern<ProjectileEnemy> bonePool;

    protected override void OnAwake()
    {
        base.OnAwake();
        boomPool.InitDefaultQuantity();
        bulletPool.InitDefaultQuantity();
        bonePool.InitDefaultQuantity();
    }
}
