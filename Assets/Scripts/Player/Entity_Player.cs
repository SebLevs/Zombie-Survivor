using UnityEngine;
using UnityEngine.InputSystem;

public class Entity_Player : Manager<Entity_Player>, IUpdateListener, IPauseListener
{
    public UserDatas UserDatas;

    public readonly int Mask = 3;

    [HideInInspector] public PermanentStats permanentStats;

    [field: Header("Variables")]
    [SerializeField] private float baseMovSpeed;
    public float MovSpeed { get; set; }
    public float BulletSpeed { get; set; }

    [field: Header("ShootControl")]
    [SerializeField] private float baseAttackSpeed;
    public float attackSpeed;
    public bool canAttack = true;
    [HideInInspector]
    public SequentialTimer attackDelay;

    [field: Header("SpecialShootControl")]
    [SerializeField] private float baseSpecialAttackSpeed;
    public float specialAttackSpeed;
    public bool canSpecialAttack = true;
    public float boomDistance = 15.0f;
    [HideInInspector]
    public SequentialTimer specialAttackDelay;

    [field: Header("DodgeControl")]
    [SerializeField] private float baseDodgeInterval;
    public float dodgeInterval;
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
    public PlayerInput Input { get; private set; }
    public Transform muzzle;
    public Transform shootFrom;
    public Collider2D col;
    public PortalArrowBehavior arrow;
    private Animator _animator;

    [field: Header("States")]
    public StateController<Entity_Player> StateController { get; private set; }
    public Player_StateContainer StateContainer { get; private set; }

    [field:Header("Health")]
    public Health Health { get; private set; }

    [field: Header("Gold")]
    private UIManager uiManager;
    public int currentGold;
    public int MaxGold = 300;

    [field: Header("Audio")]
    public PlayerAudioContainer audios;

    public PlayerAutomatedTestController AutomatedTestController { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        Rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        Controller = GetComponent<Player_Controller>();
        StateContainer = new Player_StateContainer(this);
        permanentStats = GetComponent<PermanentStats>();
        StateController = new StateController<Entity_Player>(StateContainer.State_Idle);
        DesiredActions = new PlayerActionsContainer();
        specialAttackDelay = new SequentialTimer(specialAttackSpeed);
        attackDelay = new SequentialTimer(attackSpeed);
        dodgeDelay = new SequentialTimer(dodgeInterval);
        Health = GetComponent<Health>();
        audios = GetComponent<PlayerAudioContainer>();
        Input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        AutomatedTestController = GetComponent<PlayerAutomatedTestController>();

        ResetSkillsValues();
    }
    protected override void OnStart()
    {
        base.OnStart();
        MovSpeed = 5.0f;
        BulletSpeed = 12.0f;
        uiManager = UIManager.Instance;
        
        RefreshPlayerStats();
    }

    public void ResetGold() => currentGold = 0;

    public void Reinitialize()
    {
        StateController.OnTransitionState(StateContainer.State_Idle);

        Health.FullHeal();
        transform.position = Vector3.zero;

        ResetGold();
        ResetSkillsValues();
    }

    private void ResetSkillsValues()
    {
        MovSpeed = baseMovSpeed;
        attackSpeed = baseAttackSpeed;
        specialAttackSpeed = baseSpecialAttackSpeed;
        dodgeInterval = baseDodgeInterval;
    }

    public void RefreshHealthBar()
    {
        uiManager.ViewPlayerHealthBar.Filler.SetFilling(Health.Normalized);
        uiManager.ViewPlayerHealthBar.TextElement.Element.text = Health.CurrentHP.ToString();
    }

    public void RefreshGoldBar()
    {
        float filling = (float)currentGold / (float)MaxGold;
        uiManager.ViewPlayerCurrencyBar.Filler.SetFilling(filling);
        uiManager.ViewPlayerCurrencyBar.TextElement.Element.text = currentGold + " / " + MaxGold;
    }

    public void Init()
    {
        Health.FullHeal();
        RefreshHealthBar();
        RefreshGoldBar();
    }

    public void RefreshPlayerStats()
    {
        specialAttackDelay = new SequentialTimer(specialAttackSpeed);
        attackDelay = new SequentialTimer(attackSpeed);
        dodgeDelay = new SequentialTimer(dodgeInterval);
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
        uiManager.ViewPlayerStats.DodgeDelay.Element.text = dodgeInterval.ToString("0.00");
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
            uiManager.ResetCooldownView(uiManager.ViewPlayerCooldowns.MainSkill);
        }
        else
        {
            string remainingTime = attackDelay.CurrentTime.ToString("0.00");
            float fillingNormalized = attackDelay.CurrentTime / attackDelay.TargetTime;
            uiManager.RefreshCooldownVisuals(uiManager.ViewPlayerCooldowns.MainSkill, remainingTime, fillingNormalized);
        }
        if (specialAttackDelay.HasReachedTarget())
        {
            canSpecialAttack = true;
            uiManager.ResetCooldownView(uiManager.ViewPlayerCooldowns.SecondarySkill);
        }
        else
        {
            string remainingTime = specialAttackDelay.CurrentTime.ToString("0.00");
            float fillingNormalized = specialAttackDelay.CurrentTime / specialAttackDelay.TargetTime;
            uiManager.RefreshCooldownVisuals(uiManager.ViewPlayerCooldowns.SecondarySkill, remainingTime, fillingNormalized);
        }
        if (dodgeDelay.HasReachedTarget())
        {
            canDodge = true;
            uiManager.ResetCooldownView(uiManager.ViewPlayerCooldowns.SpaceBarSkill);
        }
        else
        {
            string remainingTime = dodgeDelay.CurrentTime.ToString("0.00");
            float fillingNormalized = dodgeDelay.CurrentTime / dodgeDelay.TargetTime;
            uiManager.RefreshCooldownVisuals(uiManager.ViewPlayerCooldowns.SpaceBarSkill, remainingTime, fillingNormalized);
        }
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
        Controller.currentLookAngle = 0;
        _animator.speed = 0;
        Controller.UpdateMoveDirection(Vector2.zero);
    }

    public void OnResumeGame()
    {
        DesiredActions.PurgeAllAction();
        _animator.speed = 1;
    }
}
