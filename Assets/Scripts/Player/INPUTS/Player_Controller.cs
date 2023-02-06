using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player_Controller : MonoBehaviour
{
    private static Player_Controller instance;
    public static Player_Controller Instance => instance;

    private Entity_Player playerRef;
    private PlayerAction action;

    public Vector2 moveDirection { private set; get; }
    public Vector2 lookDirection;
    public Vector3 mousePosition;
    [SerializeField] private int currentLookDirection = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        playerRef = GetComponent<Entity_Player>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            action = new PlayerAction(PlayerActionsType.SHOOT);
            playerRef.DesiredActions.AddAction(action);
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            action = new PlayerAction(PlayerActionsType.SPECIALSHOOT);
            playerRef.DesiredActions.AddAction(action);
        }
    }


    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = new Vector2(mousePosition.x - playerRef.transform.position.x, mousePosition.y - playerRef.transform.position.y).normalized;

        if(lookDirection.x >= -0.7f && lookDirection.x <= 0.7f && lookDirection.y >= 0.7f)
        {
            currentLookDirection = 1;
        }
        else if(lookDirection.x <= -0.7f && lookDirection.y <= 0.7f && lookDirection.y >= -0.7f)
        {
            currentLookDirection = 2;
        }
        else if(lookDirection.x >= -0.7f && lookDirection.x <= 0.7f && lookDirection.y <= -0.7f)
        {
            currentLookDirection = 3;
        }
        else if(lookDirection.x >= 0.7f && lookDirection.y <= 0.7f && lookDirection.y >= -0.7f)
        {
            currentLookDirection = 4;
        }


        switch(currentLookDirection)
        {
            case 1:
                {
                    playerRef.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                }
            case 2:
                {
                    playerRef.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    break;
                }
            case 3:
                {
                    playerRef.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;
                }
            case 4:
                {
                    playerRef.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                    break;
                }
                default: { break; }
        }

    }
}
