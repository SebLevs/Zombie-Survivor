using UnityEngine;

public class Entity_Player : Manager<Entity_Player>, IFrameUpdateListener
{
    public CommandInvoker commandInvoker;

    [SerializeField] private string test;

    [field:Header("Variables")]
    public float MovSpeed { get; set; }
    public float BulletSpeed { get; set; }
    public bool isinvincible = false;

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
    public CircleCollider2D col;

    [field: Header("States")]
    public StateController<Entity_Player> StateController { get; private set; }
    public Player_StateContainer StateContainer { get; private set; }

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
    }
    protected override void OnStart()
    {
        base.OnStart();
        MovSpeed = 5.0f;
        BulletSpeed = 12.0f;
        attackDelay.JumpToTime(0f);
        specialAttackDelay.JumpToTime(0f);
        dodgeDelay.JumpToTime(0f);
        commandInvoker.DoCommand(commandInvoker.command1);
        //commandInvoker.Oncommand2();
        
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
        stateController.OnUpdate();
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
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
    }
}
