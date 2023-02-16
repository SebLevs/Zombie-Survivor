using UnityEngine;

public abstract class EnemyType : MonoBehaviour
{
    [field:Header("Type specific datas")]
    [field:SerializeField] public int Experience { get; private set; }

    protected Enemy m_context;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake() { }

    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart() { }

    public abstract void ReturnToPool();

    public void Init(Enemy context)
    {
        m_context = context;
    }
}
