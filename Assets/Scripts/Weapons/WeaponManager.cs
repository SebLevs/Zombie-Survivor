using UnityEngine;

public class WeaponManager : Manager<WeaponManager>
{
    [Header("Player pools")]
    public PoolPattern<BulletBehavior> bulletPool;
    public PoolPattern<BoomerangBehavior> boomPool;

    [Header("Enemy pools")]
    // TODO: Place into an EnemyWeaponManager.cs which would contain references to the current level projectiles
    public PoolPattern<ProjectileEnemy> bonePool;

    protected override void OnAwake()
    {
        base.OnAwake();
        boomPool.InitDefaultQuantity();
        bulletPool.InitDefaultQuantity();
        bonePool.InitDefaultQuantity();
    }
}
