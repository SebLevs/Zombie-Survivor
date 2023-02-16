using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour, IFrameUpdateListener
{
    private static Player_Controller instance;
    public static Player_Controller Instance => instance;

    private Entity_Player playerRef;
    private PlayerAction action;

    public Vector2 moveDirection { private set; get; }
    public Vector2 normalizedLookDirection;
    public Vector2 currentPlayerLookDirection;
    public Vector3 mousePosition;
    public int currentLookAngle = 0;

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

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            action = new PlayerAction(PlayerActionsType.DODGE);
            playerRef.DesiredActions.AddAction(action);
        }
    }

    public void OnOptionsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager gameManager = GameManager.Instance;
            UIManager uiManager = UIManager.Instance;

            uiManager.CurrentView.StopAllCoroutines();

            if (uiManager.CurrentView != uiManager.ViewOptionMenu)
            {
                gameManager.PauseGame();
                uiManager.OnSwitchViewSequential(uiManager.ViewOptionMenu);
            }
            else
            {
                if (SceneLoadManager.Instance.IsInTitleScreen)
                {
                    uiManager.OnSwitchViewSequential(uiManager.ViewTitleScreen);
                }
                else
                {
                    uiManager.OnSwitchViewSequential(uiManager.ViewEmpty, showCallback: () =>
                    {
                        if (!CommandPromptManager.Instance.IsActive)
                        {
                            gameManager.ResumeGame();
                        }
                    });
                }
            }
        }
    }

    public void OnUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedLookDirection = new Vector2(mousePosition.x - playerRef.transform.position.x, mousePosition.y - playerRef.transform.position.y).normalized;

        if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y >= 0.7f)
        {
            currentLookAngle = 0;
            currentPlayerLookDirection.x = 0;
            currentPlayerLookDirection.y = 1;
        }
        else if (normalizedLookDirection.x <= -0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 90;
            currentPlayerLookDirection.x = -1;
            currentPlayerLookDirection.y = 0;
        }
        else if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y <= -0.7f)
        {
            currentLookAngle = 180;
            currentPlayerLookDirection.x = 0;
            currentPlayerLookDirection.y = -1;
        }
        else if (normalizedLookDirection.x >= 0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 270;
            currentPlayerLookDirection.x = 1;
            currentPlayerLookDirection.y = 0;
        }
        playerRef.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentLookAngle));
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
    }
}
