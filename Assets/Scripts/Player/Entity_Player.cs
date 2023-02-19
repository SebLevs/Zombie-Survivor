using UnityEngine;

public class Entity_Player : Manager<Entity_Player>, IFrameUpdateListener, IPauseListener
{
    public readonly int Mask = 3;

    [SerializeField] private string test;

    [field:Header("Variables")]
    public float MovSpeed { get; set; }
    public float BulletSpeed { get; set; }
    public bool isinvincible = false;
    public bool isPermaInvincible = false;

    [field: Header("ShootControl")]
    public float attackSpeed;
    public bool canAttack = true;
    [HideInInspector]
    public SequentialTimer attackDelay;

    [field: Header("SpecialShootControl")]
    public float specialAttackSpeed;
    public bool canSpecialAttack = true;
    public float boomDistance = 15.0f;
    [HideInInspector]
    public SequentialTimer specialAttackDelay;

    [field: Header("DodgeControl")] public float DodgeInterval { get; set; }
    public bool canDodge = true;
    public float dodgeDistance;
    [HideInInspector]
    public SequentialTimer dodgeDelay;
    public AnimationCurve dodgeCurve;

    [field: Header("InputBuff")]
    [SerializeField] private PlayerAction _mAction;
    public PlayerActionsContainer DesiredActions;
    public PlayerAction Action { get => _mAction; set => _mAction = value; }

    [field: Header("References")]
    public Rigidbody2D Rb { get; private set; }
    public Player_Controller Controller { get; private set; }
    public Transform muzzle;
    public Transform shootFrom;
    public CircleCollider2D col;

    [field: Header("States")]
    public StateController<Entity_Player> StateController { get; private set; }
    public Player_StateContainer StateContainer { get; private set; }

    [field:Header("Health")]
    public Health Health { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        Rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        Controller = GetComponent<Player_Controller>();
        StateContainer = new Player_StateContainer(this);
        StateController = new StateController<Entity_Player>(StateContainer.State_Idle);
        DesiredActions = new PlayerActionsContainer();
        attackSpeed = 1.0f;
        specialAttackSpeed = 1.0f;
        DodgeInterval = 5.0f;
        specialAttackDelay = new SequentialTimer(specialAttackSpeed);
        attackDelay = new SequentialTimer(attackSpeed);
        dodgeDelay = new SequentialTimer(DodgeInterval);
        Health = GetComponent<Health>();
    }
    protected override void OnStart()
    {
        base.OnStart();
        MovSpeed = 5.0f;
        BulletSpeed = 12.0f;
        attackDelay.JumpToTime(0f);
        specialAttackDelay.JumpToTime(0f);
        dodgeDelay.JumpToTime(0f);
        //Init();
    }

    public void RefreshHealthBar()
    {
        UIManager uIManager = UIManager.Instance;
        if (uIManager != null)
        {
            uIManager.ViewPlayerHealthBar.Filler.SetFilling(Health.Normalized);
            uIManager.ViewPlayerHealthBar.Counter.Element.text = Health.CurrentHP.ToString();
        }
    }

    public void RefreshExperienceBar()
    {

    }

    public void Init()
    {
        Health.FullHeal();
        if (UIManager.Instance.ViewPlayerHealthBar.gameObject.activeSelf) { RefreshHealthBar(); }
        transform.position = Vector3.zero;
    }

    public void RefreshWeaponStats()
    {
        if(attackDelay.TargetTime != attackSpeed)
        {
            attackDelay = new SequentialTimer(attackSpeed);
        }
    }

    public void OnUpdate()
    {
        Debug.Log(shootFrom.position.ToString());
        StateController.OnUpdate();
        DesiredActions.OnUpdateActions();
        attackDelay.OnUpdateTime();
        specialAttackDelay.OnUpdateTime();
        dodgeDelay.OnUpdateTime();
        if (attackDelay.HasReachedTarget())
        {
            canAttack = true;
        }
        if (specialAttackDelay.HasReachedTarget())
        {
            canSpecialAttack = true;
        }
        if (dodgeDelay.HasReachedTarget())
        {
            canDodge = true;
        }
        RefreshWeaponStats();
        test = StateController.CurrentState.ToString();
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }

        if (GameManager.Instance)
        {
            GameManager.Instance.UnSubscribeFromPauseGame(this);
        }
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
        GameManager.Instance.SubscribeToPauseGame(this);
    }

    public void OnPauseGame()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero); // TODO: Temporary fix for sprite rotating on pause, might be fixed when player prefab is completed
        Rb.velocity = Vector2.zero;
        col.enabled = false;
        Controller.currentLookAngle = 0;
    }

    public void OnResumeGame()
    {
        col.enabled = true;
        DesiredActions.PurgeAllAction();
    }
}
