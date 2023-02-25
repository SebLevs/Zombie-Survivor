using UnityEngine;

public class ProjectileEnemyGroundSmash : ProjectileEnemy
{
    [SerializeField] private ParticleSystem m_particles;

    public override ProjectileEnemy GetFromPool(Transform spawnLocation)
    {
        return WeaponManager.Instance.groundSmashPool.GetFromAvailable(spawnLocation.position, spawnLocation.rotation);
    }

    public override void ReturnToPool()
    {
        WeaponManager.Instance.groundSmashPool.ReturnToAvailable(this);
    }

    public override void OnGetFromAvailable()
    {
        base.OnGetFromAvailable();
        m_particles.Play();
    }

    public override void OnReturnToAvailable()
    {
        base.OnReturnToAvailable();
        m_particles.Stop();
    }

    public override void OnPauseGame()
    {
        base.OnPauseGame();
        m_particles.Pause();
    }

    public override void OnResumeGame()
    {
        base.OnResumeGame();
        m_particles.Play();
    }

    protected override void OnEntityTriggerEnter(Collider2D collision)
    {
        base.OnEntityTriggerEnter(collision);

        if (!IsOtherLayerAlsoTargetLayer(collision.gameObject.layer, _targetMask)) { return; }

        Health health = collision.GetComponent<Health>();
        if (health)
        {
            health.Hit(m_damage);
        }
    }
}
