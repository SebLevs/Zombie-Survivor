using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable, IFrameUpdateListener
{
    public EnemyType Type { get; private set; }

    [Header("Health bar")]
    [SerializeField] private ViewHealthBarWithCounter m_healthBar;
    public Health Health { get; private set; }

    protected Rigidbody2D m_rigidbody;
    protected Collider2D m_collider;

    protected EnemyStateController m_stateController;
    public PathfinderUtility PathfinderUtility { get; private set; }

    [field: SerializeField] public int collisionDamage { get; private set; }
    [field: SerializeField] public int TempDamage { get; private set; }

    private void Awake()
    {
        Type = GetComponent<EnemyType>();

        Health = GetComponent<Health>();

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();

        m_stateController = GetComponent<EnemyStateController>();

        PathfinderUtility = GetComponent<PathfinderUtility>();
    }

    private void Start()
    {
        Init();
    }

    public void OnGetFromAvailable()
    {
        EnemyManager.Instance.CurrentlyActiveEnemies.Add(this);
    }

    public void OnReturnToAvailable()
    {
        Init();
        EnemyManager.Instance.CurrentlyActiveEnemies.Remove(this);
    }

    public void ReturnToPool() => Type.ReturnToPool(this);

    private void Init()
    {
        m_rigidbody.velocity = Vector2.zero;
        Health.FullHeal();
        m_healthBar.Filler.SetFilling(Health.Normalized);
        m_healthBar.Counter.Element.text = Health.CurrentHP.ToString();
    }

    public void Kill()
    {
        Health.OnInstantDeath();
    }

    public void OnUpdate()
    {
        // TODO: Call state controller here
        m_stateController.OnUpdate();
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

}
