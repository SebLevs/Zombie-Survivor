using UnityEngine;

public class TypeSkeleton : EnemyType
{
    [SerializeField] private SkillProjectileInstantiator m_projectileInstantiator;

    public override void ReturnToPool()
    {
        EnemyManager.Instance.Skeletons.ReturnToAvailable(m_context);
    }

    public void AEShootProjectile() // Animation Event
    {
        ProjectileEnemy projectile = m_projectileInstantiator.GetProjectileFromPool();
        projectile.SetTargetAsPlayer();
        projectile.ShootTowardsTarget();
    }

    public void AESetAndShootProjectile(ProjectileEnemy projectile) // Animation Event
    {
        m_projectileInstantiator.SetProjectileReference(projectile);
        AEShootProjectile();
    }
}
