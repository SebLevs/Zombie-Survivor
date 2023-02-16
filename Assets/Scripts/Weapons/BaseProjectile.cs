using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour, IFrameUpdateListener, IFixedUpdateListener
{
    [SerializeField] protected int m_damage = 1;

    [field:SerializeField] [field:Min(0)] public int TargetMask { get; set; }
    protected bool EvaluateLayers(int otherLayer, int targetLayer)
    {
        return otherLayer == targetLayer;
    }

    protected abstract void OnStart();
    protected abstract void OnAwake();
    protected abstract void OnUpdate();
    protected virtual void OnFixedUpdate() { }

    protected virtual void OnProjectileCollisionEnter(Collision2D collision) { }
    protected virtual void OnProjectileCollisionLeave(Collision2D collision) { }
    protected virtual void OnProjectileTriggerEnter(Collider2D collision) { }
    protected virtual void OnProjectileTriggerLeave(Collider2D collision) { }

    private void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnProjectileCollisionEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnProjectileCollisionLeave(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnProjectileTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnProjectileTriggerLeave(collision);
    }

    public virtual void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
        UpdateManager.Instance.SubscribeToFixedUpdate(this);
    }

    public virtual void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
            UpdateManager.Instance.UnSubscribeFromFixedUpdate(this);
        }
    }

    void IFrameUpdateListener.OnUpdate()
    {
        OnUpdate();
    }

    void IFixedUpdateListener.OnFixedUpdate()
    {
        OnFixedUpdate();
    }
}
