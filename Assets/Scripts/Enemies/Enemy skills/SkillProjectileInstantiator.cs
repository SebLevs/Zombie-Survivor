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
}
