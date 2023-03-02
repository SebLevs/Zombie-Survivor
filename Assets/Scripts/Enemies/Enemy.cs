using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable, IUpdateListener, IPauseListener
{
    public EnemyType Type { get; private set; }

    [SerializeField] private bool isSpriteFlippable = true;

    [Header("Health bar")]
    [SerializeField] private ViewFillingBarWithTextElement m_healthBar;
    public Health Health { get; private set; }

    protected Rigidbody2D m_rigidbody;
    protected Collider2D m_collider;

    protected SpriteRenderer m_spriteRenderer;
    public Animator Animator { get; protected set; }

    protected EnemyStateController m_stateController;
    public PathfinderUtility PathfinderUtility { get; private set; }

    private Entity_Player player;

    [Header("Return to pool")]
    [SerializeField] private bool isReturnToPoolAtDistance = true;
    [SerializeField] private float returnAtDistance = 100f;

    [SerializeField] private AudioElement attackSounds;

    private void Awake() { OnAwake(); }

    protected void OnAwake()
    {
        Type = GetComponent<EnemyType>();

        Health = GetComponent<Health>();

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();

        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animator = GetComponent<Animator>();

        m_stateController = GetComponent<EnemyStateController>();

        PathfinderUtility = GetComponent<PathfinderUtility>();

        Type.Init(this);
    }

    private void Start() { OnStart(); }
    protected virtual void OnStart()
    {
        Init();
    }

    private void Init()
    {
        m_rigidbody.velocity = Vector2.zero;
        Health.FullHeal();
        m_healthBar.OnHideQuick();
        player = Entity_Player.Instance;
        SetColliderEnable(true);
    }

    public virtual void OnGetFromAvailable()
    {
        Init();
        m_stateController.OnTransitionState(m_stateController.GetDefaultState());
        EnemyManager.Instance.CurrentlyActiveEnemies.Add(this);
    }

    public void AECallAttackAudio()
    {
        attackSounds.PlayRandom();
    }


    public virtual void OnReturnToAvailable()
    {
        EnemyManager.Instance.CurrentlyActiveEnemies.Remove(this);
    }

    public void ReturnToPool() { Type.ReturnToPool(); } // Currently used as an animation event on death

    public void Kill() 
    {
        Health.OnInstantDeath();
    }

    public virtual void OnUpdate()
    {
        m_stateController.OnUpdate();
        float angle = MathAngleUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, transform);
        int angleIndex = MathAngleUtilities.GetAngleAsIndex2D_Quad(angle);
        if (isSpriteFlippable) { FlipSpriteHorizontally(angleIndex); }
        Animator.SetFloat("angle", angleIndex);

        EvaluateReturnToPoolFromDistance();
    }

    public virtual void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
        if(GameManager.Instance)
        {
            GameManager.Instance.UnSubscribeFromPauseGame(this);
        }
    }

    public virtual void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
        GameManager.Instance.SubscribeToPauseGame(this);
    }

    public virtual void OnPauseGame()
    {
        Animator.speed = 0f;

        PathfinderUtility.DisablePathfinding();

        m_rigidbody.velocity = Vector2.zero;
        if (m_collider) { m_collider.enabled = false; }
    }

    public virtual void OnResumeGame()
    {
        Animator.speed = 1f;

        if (m_collider) { m_collider.enabled = true; }

        PathfinderUtility.EnablePathfinding();
    }

    public void OnFirstHitPopupHealthBar()
    {
        if (!m_healthBar.gameObject.activeSelf) // !Health.IsDead && 
        {
            m_healthBar.StopAllCoroutines();
            m_healthBar.OnShowQuick();
        }
    }

    private void EvaluateReturnToPoolFromDistance()
    {
        if (!isReturnToPoolAtDistance) { return; }
        float distance = LinearAlgebraUtilities.GetDistance2D(player.transform.position, transform.position);
        if (distance >= returnAtDistance)
        {
            ReturnToPool();
        }
    }

    public void OnStopAllCoroutines()
    {
        StopAllCoroutines();
        Health.StopAllCoroutines();
        m_healthBar.StopAllCoroutines();
    }

    public void SetColliderEnable(bool setAs)
    {
        if (m_collider == null) { return; }
        m_collider.enabled = setAs;
    }

    /// <summary>
    /// Currently using a counterclockwise quardinal setup where index at right = 0 and up = 1
    /// </summary>
    public void FlipSpriteHorizontally(int angleIndex)
    {
        m_spriteRenderer.flipX = (angleIndex == 2) ? true: false;
    }
}
