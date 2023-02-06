using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player : Manager<Entity_Player>
{
    [field:Header("Variables")]
    public float movSpeed { get; set; }
    public float bulletSpeed { get; set; }

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
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        controller = GetComponent<Player_Controller>();
        stateContainer = new Player_StateContainer(this);
        stateController = new StateController<Entity_Player>(stateContainer.State_Idle);
        DesiredActions = new PlayerActionsContainer();
    }
    protected override void OnStart()
    {
        base.OnStart();
        movSpeed = 5.0f;
        bulletSpeed = 7.0f;
    }

    private void Update()
    {
        stateController.OnUpdate();
        DesiredActions.OnUpdateActions();
    }
}
