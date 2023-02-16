using UnityEngine;

public abstract class Skill : BaseCollisionHandler, IPoolable
{
    protected GameObject m_invoker;

    public abstract Skill GetFromPool(Vector3 position, Quaternion rotation);
    public abstract void ReturnToPool();

    public abstract void OnGetFromAvailable();

    public virtual void OnReturnToAvailable() { m_invoker = null; }

    public void Init(GameObject invoker, int targetMask)
    {
        m_invoker = invoker;
        _targetMask = targetMask;
    }
}
