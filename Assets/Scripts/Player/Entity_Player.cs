using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player : Manager<Entity_Player>
{
    public CommandInvoker commandInvoker;

    [SerializeField] private string test;
    [field:Header("Variables")]
    public float movSpeed { get; set; }
    public float bulletSpeed { get; set; }
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

    [field: Header("DodgeControl")]
    public float dodgeInterval { get; set; }
    public bool canDodge = true;
    public float dodgeDistance;
    [HideInInspector]
    public SequentialTimer dodgeDelay;
    public AnimationCurve dodgeCurve;

    [field: Header("InputBuff")]
    [SerializeField] private PlayerAction m_action;
    public PlayerActionsContainer DesiredActions;
    public PlayerAction Action { get => m_action; set => m_action = value; }

    [field: Header("References")]
    public Rigidbody2D rb { get; private set; }
    public Player_Controller controller { get; private set; }
    public Transform muzzle;
    public CircleCollider2D col;

    [field: Header("States")]
    public StateController<Entity_Player> stateController { get; private set; }
    public Player_StateContainer stateContainer { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        commandInvoker = new CommandInvoker(new PlayerCommand_Invincibility(this), new PlayerCommand_AttackSpeedUp(this));
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        controller = GetComponent<Player_Controller>();
        stateContainer = new Player_StateContainer(this);
        stateController = new StateController<Entity_Player>(stateContainer.State_Idle);
        DesiredActions = new PlayerActionsContainer();
        attackSpeed = 1.0f;
        specialAttackSpeed = 1.0f;
        dodgeInterval = 5.0f;
        specialAttackDelay = new SequentialTimer(specialAttackSpeed);
        attackDelay = new SequentialTimer(attackSpeed);
        dodgeDelay = new SequentialTimer(dodgeInterval);
    }
    protected override void OnStart()
    {
        base.OnStart();
        movSpeed = 5.0f;
        bulletSpeed = 12.0f;
        attackDelay.JumpToTime(0f);
        specialAttackDelay.JumpToTime(0f);
        dodgeDelay.JumpToTime(0f);
        commandInvoker.DoCommand(commandInvoker.command1);
        //commandInvoker.Oncommand2();
        
    }

    private void Update()
    {
        
        stateController.OnUpdate();
        DesiredActions.OnUpdateActions();
        attackDelay.OnUpdateTime();
        specialAttackDelay.OnUpdateTime();
        dodgeDelay.OnUpdateTime();
        if(attackDelay.HasReachedTarget())
        {
            canAttack = true;
        }
        if (specialAttackDelay.HasReachedTarget())
        {
            canSpecialAttack = true;
        }
        if(dodgeDelay.HasReachedTarget())
        {
            canDodge = true;
        }
        RefreshWeaponStats();
        test = stateController.CurrentState.ToString();
    }
    public void RefreshWeaponStats()
    {
        if(attackDelay.TargetTime != attackSpeed)
        {
            attackDelay = new SequentialTimer(attackSpeed);
        }
    }

}
