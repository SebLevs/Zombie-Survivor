using UnityEngine;

public class SkillProjectileInstantiator : MonoBehaviour
{
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private ProjectileEnemy _projectileReference;
    public ProjectileEnemy CurrentProjectile { get; private set; }

    public void SetProjectileReference(ProjectileEnemy projectile) => _projectileReference = projectile;

    public ProjectileEnemy GetProjectileFromPool()
    {
        CurrentProjectile = _projectileReference.GetFromPool(_spawnLocation);
        return CurrentProjectile;
    }

    public void SetTargetAsPlayer()
    {
        CurrentProjectile?.SetTargetAsPlayer();
    }

    public void AESetAndShootProjectileAtPlayer(ProjectileEnemy projectile) // Animation Event
    {
        SetProjectileReference(projectile);
        ProjectileEnemy currentProjectile = GetProjectileFromPool();
        currentProjectile.SetTargetAsPlayer();
        currentProjectile.ShootTowardsTarget();
    }

    public void AESetAndSpawnStaticProjectile(ProjectileEnemy projectile) // Animation Event
    {
        SetProjectileReference(projectile);
        GetProjectileFromPool();
    }

    public void AEReturnProjectileToPool() // Animation Event
    {
        CurrentProjectile.ReturnToPool();
    }
}
