using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player : MonoBehaviour
{
    public float movSpeed { get; set; }

    public Rigidbody2D rb { get; private set; }
    public Player_Controller controller { get; private set; }


    public StateController<Entity_Player> stateController { get; private set; }
    public Player_StateContainer stateContainer { get; private set; }

    private void Start()
    {
        movSpeed = 5.0f;
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Player_Controller>();
        stateContainer = new Player_StateContainer(this);
        stateController = new StateController<Entity_Player>(stateContainer.State_Idle);
    }
    private void Update()
    {
        stateController.OnUpdate();
    }
}
