using UnityEngine;
using UnityEngine.InputSystem;

public enum LookDirectionEnum
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class Player_Controller : MonoBehaviour, IFrameUpdateListener
{
    private static Player_Controller instance;
    public static Player_Controller Instance => instance;

    private Entity_Player playerRef;
    private PlayerAction action;

    public Vector2 moveDirection { private set; get; }
    public Vector2 normalizedLookDirection;
    public Vector3 mousePosition;
    public int currentLookAngle = 0;

    private SpriteRenderer sp;
    public Sprite[] spriteDirection;
    public Sprite currentSprite;
    private Animator anim;
    private static readonly int Velocity = Animator.StringToHash("Velocity");

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
        currentSprite = spriteDirection[0];
        sp = playerRef.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
        if (context.performed && !Entity_Player.Instance.Health.IsDead)
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

    private LookDirectionEnum GetDirection(Vector2 normalizedDirection)
    {
        float angle = Mathf.Atan2(normalizedDirection.x, normalizedDirection.y) * Mathf.Rad2Deg;
        switch (angle)
        {
            case >= -45 and <= 45:
                return LookDirectionEnum.UP;
            case >= 45 and <= 135:
                return LookDirectionEnum.RIGHT;
            case >= 135 and <= 180:
            case <= -135 and >= -180:
                return LookDirectionEnum.DOWN;
            default:
                return LookDirectionEnum.LEFT;
        }
    }

    public void OnUpdate()
    {
        // Prevent mouse movement visuals when dead (game is not paused currently on death)
        if (Entity_Player.Instance.Health.IsDead) { return; } 

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedLookDirection = new Vector2(mousePosition.x - playerRef.transform.position.x, mousePosition.y - playerRef.transform.position.y).normalized;

        // switch (GetDirection(normalizedLookDirection))
        // {
        //     case LookDirectionEnum.UP:
        //     {
        //         currentSprite = spriteDirection[0];
        //         break;
        //     }
        //     case LookDirectionEnum.RIGHT:
        //     {
        //         currentSprite = spriteDirection[1];
        //         break;
        //     }
        //     case LookDirectionEnum.DOWN:
        //     {
        //         currentSprite = spriteDirection[2];
        //         break;
        //     }
        //     case LookDirectionEnum.LEFT:
        //     {
        //         currentSprite = spriteDirection[1];
        //         //playerRef.transform.localScale = new Vector3(1, 0, 0);
        //         break;
        //     }
        // }
        
        if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y >= 0.7f)
        {
            currentLookAngle = 0;
            //currentSprite = spriteDirection[0];
        }
        else if (normalizedLookDirection.x <= -0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 90;
            //currentSprite = spriteDirection[1];
            playerRef.transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y <= -0.7f)
        {
            currentLookAngle = 180;
            //currentSprite = spriteDirection[2];
        }
        else if (normalizedLookDirection.x >= 0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 270;
            //currentSprite = spriteDirection[3]; 
        }
        anim.SetInteger("LookDirection", currentLookAngle);
        anim.SetFloat(Velocity, Entity_Player.Instance.Rb.velocity.magnitude);
        playerRef.muzzle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentLookAngle));
        //sp.sprite = currentSprite;
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
