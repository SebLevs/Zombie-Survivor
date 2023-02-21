using UnityEngine;

public class Entity_Player : Manager<Entity_Player>, IFrameUpdateListener, IPauseListener
{
    public readonly int Mask = 3;

    [SerializeField] private string test;

    [field:Header("Variables")]
    public float MovSpeed { get; set; }
    public float BulletSpeed { get; set; }

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

    [field: Header("DodgeControl")] public float DodgeInterval;
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

    [field: Header("Gold")]
    private UIManager uiManager;
    public int currentGold;
    public int MaxGold = 300;

    protected override void OnAwake()
    {
        base.OnAwake();
        Rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        Controller = GetComponent<Player_Controller>();
        StateContainer = new Player_StateContainer(this);
        StateController = new StateController<Entity_Player>(StateContainer.State_Idle);
        DesiredActions = new PlayerActionsContainer();
        specialAttackDelay = new SequentialTimer(specialAttackSpeed);
        attackDelay = new SequentialTimer(attackSpeed);
        dodgeDelay = new SequentialTimer(DodgeInterval);
        Health = GetComponent<Health>();
        uiManager = UIManager.Instance;
    }
    protected override void OnStart()
    {
        base.OnStart();
        MovSpeed = 5.0f;
        BulletSpeed = 12.0f;
        RefreshPlayerStats();
        //Init();
    }

    public void RefreshHealthBar()
    {
        if (uiManager != null)
        {
            uiManager.ViewPlayerHealthBar.Filler.SetFilling(Health.Normalized);
            uiManager.ViewPlayerHealthBar.Counter.Element.text = Health.CurrentHP.ToString();
        }
    }

    public void RefreshExperienceBar()
    {
        if (uiManager != null)
        {
            float filling = currentGold / MaxGold;
            uiManager.ViewPlayerExperienceBar.Filler.SetFilling(filling);
            uiManager.ViewPlayerExperienceBar.Counter.Element.text = currentGold + " / " + MaxGold;
        }
    }

    public void Init()
    {
        Health.FullHeal();
        RefreshHealthBar();
    }

    public void RefreshPlayerStats()
    {
        specialAttackDelay = new SequentialTimer(specialAttackSpeed);
        attackDelay = new SequentialTimer(attackSpeed);
        dodgeDelay = new SequentialTimer(DodgeInterval);
        attackDelay.JumpToTime(0f);
        specialAttackDelay.JumpToTime(0f);
        dodgeDelay.JumpToTime(0f);

        RefreshPlayerStatsUI();
    }

    public void RefreshPlayerStatsUI()
    {
        if (!uiManager.ViewPlayerStats.gameObject.activeSelf) { return; }

        uiManager.ViewPlayerStats.Invincibility.Element.text = Health.isPermaInvincible.ToString();
        uiManager.ViewPlayerStats.MoveSpeed.Element.text = MovSpeed.ToString("0.00");
        uiManager.ViewPlayerStats.AttackCooldown.Element.text = attackSpeed.ToString("0.00");
        uiManager.ViewPlayerStats.BoomerangCooldown.Element.text = specialAttackSpeed.ToString("0.00");
        uiManager.ViewPlayerStats.BoomerangDistance.Element.text = boomDistance.ToString("0.00");
    }

    public void OnUpdate()
    {
        StateController.OnUpdate();
        DesiredActions.OnUpdateActions();
        attackDelay.OnUpdateTime();
        specialAttackDelay.OnUpdateTime();
        dodgeDelay.OnUpdateTime();
        if (attackDelay.HasReachedTarget())
        {
            canAttack = true;
            uiManager.ResetCooldownView(uiManager.ViewPlayerCooldowns.m_mainSkill);
        }
        else
        {
            string remainingTime = attackDelay.CurrentTime.ToString("0.00");
            float fillingNormalized = attackDelay.CurrentTime / attackDelay.TargetTime;
            uiManager.RefreshCooldownVisuals(uiManager.ViewPlayerCooldowns.m_mainSkill, remainingTime, fillingNormalized);
        }
        if (specialAttackDelay.HasReachedTarget())
        {
            canSpecialAttack = true;
            uiManager.ResetCooldownView(uiManager.ViewPlayerCooldowns.m_secondarySkill);
        }
        else
        {
            string remainingTime = specialAttackDelay.CurrentTime.ToString("0.00");
            float fillingNormalized = specialAttackDelay.CurrentTime / specialAttackDelay.TargetTime;
            uiManager.RefreshCooldownVisuals(uiManager.ViewPlayerCooldowns.m_secondarySkill, remainingTime, fillingNormalized);
        }
        if (dodgeDelay.HasReachedTarget())
        {
            canDodge = true;
            uiManager.ResetCooldownView(uiManager.ViewPlayerCooldowns.m_TertiarySkill);
        }
        else
        {
            string remainingTime = dodgeDelay.CurrentTime.ToString("0.00");
            float fillingNormalized = dodgeDelay.CurrentTime / dodgeDelay.TargetTime;
            uiManager.RefreshCooldownVisuals(uiManager.ViewPlayerCooldowns.m_TertiarySkill, remainingTime, fillingNormalized);
        }
        //RefreshPlayerStats();
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
        //col.enabled = false;
        Controller.currentLookAngle = 0;
        
    }

    public void OnResumeGame()
    {
        //col.enabled = true;
        DesiredActions.PurgeAllAction();
    }
}
