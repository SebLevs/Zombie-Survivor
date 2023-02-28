using UnityEngine;

public class ProjectileEnemyGroundSmash : ProjectileEnemy
{
    private ParticleSystem m_particles;

    [Header("Collider management")]
    [SerializeField] [Min(0)] private float colliderActiveTime = 1f;
    private SequentialTimer m_disablecolliderTimer;

    protected override void OnAwake()
    {
        m_particles = GetComponent<ParticleSystem>();
        m_disablecolliderTimer = new SequentialTimer(colliderActiveTime, () =>
        {
            m_collider.enabled = false;
        });
        returnToPoolTime = m_particles.main.duration + m_particles.main.startLifetime.constant;
        base.OnAwake();
    }

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
        m_collider.enabled = true;
        m_particles.Play();
        m_disablecolliderTimer.StartTimer();
    }

    public override void OnReturnToAvailable()
    {
        base.OnReturnToAvailable();
        m_particles.Stop();
        m_disablecolliderTimer.Reset();
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

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_disablecolliderTimer.OnUpdateTime();
    }

    protected override void OnEntityTriggerEnter(Collider2D collision)
    {
        base.OnEntityTriggerEnter(collision);

        if (!IsValidForInteract(collision.gameObject.layer, collision.gameObject.tag)) { return; }

        Health health = collision.GetComponent<Health>();
        if (health)
        {
            health.Hit(m_damage);
        }
    }
}
