using UnityEngine;

public abstract class ProjectileEnemy : BaseCollisionHandler, IPoolable, IPauseListener
{
    [Header("Damage")]
    [SerializeField] protected int m_damage;

    [Header("Movement")]
    [SerializeField] protected float m_baseSpeed;
    protected Vector2 m_oldVelocity;

    public Transform Target { get; set; }

    public abstract ProjectileEnemy GetFromPool(Transform spawnLocation);
    public abstract void ReturnToPool();

    public void SetTargetLayerMask(int layerMask)
    {
        _targetMask = layerMask;
    }

    public void SetTargetAsPlayer()
    {
        Target = Entity_Player.Instance.transform;
    }

    public void SetTargetAs(Transform target)
    {
        Target = target;
    }

    public void ShootTowardsTarget()
    {
        Vector2 direction = (Target.position - transform.position).normalized;
        transform.up = direction;
        m_rigidBody.velocity = direction * m_baseSpeed;
    }

    public void ShootTowards(Vector2 direction)
    {
        transform.up = direction;
        m_rigidBody.velocity = direction * m_baseSpeed;
    }

    public void StopMovement() => m_rigidBody.velocity = Vector2.zero;

    public void ResumeMovement() => m_rigidBody.velocity = m_oldVelocity;

    public void SaveVelocity() => m_oldVelocity = m_rigidBody.velocity;

    public virtual void OnGetFromAvailable()
    {
    }

    public virtual void OnReturnToAvailable()
    {
        StopMovement();
    }

    public void OnEnable()
    {
        GameManager.Instance.SubscribeToPauseGame(this);
    }

    public void OnDisable()
    {
        GameManager.Instance.UnSubscribeFromPauseGame(this);
    }

    public void OnPauseGame()
    {
        SaveVelocity();
        StopMovement();
    }

    public void OnResumeGame()
    {
        ResumeMovement();
    }
}
