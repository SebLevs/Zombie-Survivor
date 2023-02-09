using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private ViewHealthBarWithCounter m_healthBar;
    private Health m_hp;

    public PoolPattern<Enemy> PoolRef { get; set; }

    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;

    private void Awake()
    {
        m_hp = GetComponent<Health>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
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
        ReturnToPool();
    }
    private void ReturnToPool() => PoolRef?.ReturnToAvailable(this);

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
}
