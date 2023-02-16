using UnityEngine;


public class BulletBehavior : BaseProjectile, IPoolable, IPauseListener
{
    public bool playerIsShooting = true;
    public Rigidbody2D rb;
    public CapsuleCollider2D col;
    [SerializeField] private SequentialStopwatch destroyStopWatch;
    [SerializeField] private float timeToDestroy;

    protected override void OnStart()
    {
    }

    protected override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        timeToDestroy = 2.0f;
        destroyStopWatch = new SequentialStopwatch(timeToDestroy);
    }

    protected override void OnProjectileCollisionEnter(Collision2D collision)
    {
        base.OnProjectileCollisionEnter(collision);

        if (playerIsShooting) // (EvaluateLayers(collision.gameObject.layer, GetTargetMaskValue))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health)
            {
                health.Hit(m_damage);
            }
        }
        WeaponManager.Instance.bulletPool.ReturnToAvailable(this);
    }

    protected override void OnUpdate()
    {
        destroyStopWatch.OnUpdateTime();
        if (destroyStopWatch.HasReachedTarget())
        {
            WeaponManager.Instance.bulletPool.ReturnToAvailable(this);
        }
    }

    public void OnGetFromAvailable()
    {
        if(playerIsShooting)
        {
            Physics2D.IgnoreCollision(col, Entity_Player.Instance.col);
        }
/*        else
        {
            Physics2D.IgnoreCollision(col, null);
        }*/
        destroyStopWatch.StartTimer();
    }

    public void OnReturnToAvailable()
    {
        rb.velocity = Vector2.zero;
        destroyStopWatch.Reset(true);
        playerIsShooting = false;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.SubscribeToPauseGame(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.UnSubscribeFromPauseGame(this);
    }

    public void ShootBullet(Vector2 direction, float speed)
    {
        transform.up = direction;
        rb.velocity = direction * speed;
    }

    Vector2 m_lastVelocity;
    public void OnPauseGame()
    {
        m_lastVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        col.enabled = false;
    }

    public void OnResumeGame()
    {
        rb.velocity = m_lastVelocity;
        col.enabled = true;
    }
}
