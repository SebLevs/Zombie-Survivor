using UnityEngine;
using UnityEngine.InputSystem;

public enum LookDirectionEnum
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class Player_Controller : MonoBehaviour, IUpdateListener
{
    private static Player_Controller instance;
    public static Player_Controller Instance => instance;

    private Entity_Player playerRef;
    private PlayerAction action;

    public Vector2 moveDirection { private set; get; }
    public void UpdateMoveDirection(Vector2 moveDirection) => this.moveDirection = moveDirection;
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            action = new PlayerAction(PlayerActionsType.INTERACT);
            playerRef.DesiredActions.AddAction(action);
        }
    }

    public void OnOptionsMenu(InputAction.CallbackContext context)
    {
        // TODO: Refactor into a player state if time: conditionnal check is becoming cancerous
        if (context.performed && !Entity_Player.Instance.Health.IsDead 
            && !UIManager.Instance.ViewLoadingScreen.gameObject.activeSelf 
            && !UIManager.Instance.ViewLogin.gameObject.activeSelf)
        {
            GameManager gameManager = GameManager.Instance;
            UIManager uiManager = UIManager.Instance;

            uiManager.ViewController.CurrentView.StopAllCoroutines();

            if (uiManager.ViewController.CurrentView != uiManager.ViewOptionMenu)
            {
                gameManager.PauseGame();
                uiManager.ViewController.SwitchViewSequential(uiManager.ViewOptionMenu);
            }
            else
            {
                if (SceneLoadManager.Instance.IsInTitleScreen)
                {
                    uiManager.ViewController.SwitchViewSequential(uiManager.ViewTitleScreen);
                }
                else
                {
                    uiManager.ViewController.SwitchViewSequential(uiManager.ViewEmpty, showCallback: () =>
                    {
                        if (!CommandPromptManager.Instance.isActive)
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

    public void SetLookAt(Vector3 targetTransform)
    {
        if (Entity_Player.Instance.Health.IsDead) { return; }

        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //normalizedLookDirection = new Vector2(mousePosition.x - playerRef.transform.position.x, mousePosition.y - playerRef.transform.position.y).normalized;
        Vector2 direction = targetTransform - playerRef.transform.position;
        normalizedLookDirection = direction.normalized;

        if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y >= 0.7f)
        {
            currentLookAngle = 0;
        }
        else if (normalizedLookDirection.x <= -0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 90;
        }
        else if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y <= -0.7f)
        {
            currentLookAngle = 180;
        }
        else if (normalizedLookDirection.x >= 0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 270;
        }
        anim.SetInteger("LookDirection", currentLookAngle);
        anim.SetFloat(Velocity, Entity_Player.Instance.Rb.velocity.magnitude);
        playerRef.muzzle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentLookAngle));
    }

    public void OnUpdate()
    {
        // Prevent mouse movement visuals when dead (game is not paused currently on death)
        if (Entity_Player.Instance.Health.IsDead) { return; } 

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedLookDirection = new Vector2(mousePosition.x - playerRef.transform.position.x, mousePosition.y - playerRef.transform.position.y).normalized;

        if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y >= 0.7f)
        {
            currentLookAngle = 0;
        }
        else if (normalizedLookDirection.x <= -0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 90;
            // playerRef.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (normalizedLookDirection.x >= -0.7f && normalizedLookDirection.x <= 0.7f && normalizedLookDirection.y <= -0.7f)
        {
            currentLookAngle = 180;
        }
        else if (normalizedLookDirection.x >= 0.7f && normalizedLookDirection.y <= 0.7f && normalizedLookDirection.y >= -0.7f)
        {
            currentLookAngle = 270;
        }
        anim.SetInteger("LookDirection", currentLookAngle);
        anim.SetFloat(Velocity, Entity_Player.Instance.Rb.velocity.magnitude);
        playerRef.muzzle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentLookAngle));
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
