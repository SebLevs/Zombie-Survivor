using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable, IFrameUpdateListener
{
    EnemyType m_type;

    [Header("Health bar")]
    [SerializeField] private ViewHealthBarWithCounter m_healthBar;
    private Health m_hp;

    public PathfinderUtility PathfinderUtility { get; private set; }

    public PoolPattern<Enemy> PoolRef { get; set; } // TODO: Refactory into a type object pattern with direct pool return by call instead of reference

    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;

    private void Awake()
    {
        m_type = GetComponent<EnemyType>();

        m_hp = GetComponent<Health>();
        
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();

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

    public void ReturnToPool() => m_type.ReturnToPool(this);

    private void Init()
    {
        m_rigidbody.velocity = Vector2.zero;
        m_hp.FullHeal();
        m_healthBar.Filler.SetFilling(m_hp.Normalized);
        m_healthBar.Counter.Element.text = m_hp.CurrentHP.ToString();
    }

    public void Kill()
    {
        m_hp.OnInstantDeath();
    }

    public void OnUpdate()
    {
        //StateController.OnUpdate();
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
