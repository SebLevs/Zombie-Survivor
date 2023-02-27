using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandPromptManager : Manager<CommandPromptManager>
{
    public CommandInvoker playerCommandInvoker;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text doneCommands;
    public bool isActive;
    private string _isValidCommand;
    private string _inputCommand;

    protected override void OnAwake()
    {
        playerCommandInvoker.Init();
    }

    protected override void OnStart()
    {
        base.OnStart();
        isActive = false;
    }

    public void ToggleActivatePrompt(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isActive)
            {
                if (!SceneLoadManager.Instance.IsInTitleScreen &&
                    UIManager.Instance.CurrentView != UIManager.Instance.ViewOptionMenu)
                {
                    GameManager.Instance.ResumeGame();
                }

                DeActivate();
            }
            else
            {
                GameManager.Instance.PauseGame();
                Activate();
            }
        }
    }

    public void Activate()
    {
        Entity_Player.Instance.Controller.enabled = false;
        Entity_Player.Instance.enabled = false;
        inputField.gameObject.SetActive(true);
        doneCommands.gameObject.SetActive(true);
        isActive = true;
        inputField.Select();
    }

    public void DeActivate()
    {
        Entity_Player.Instance.Controller.enabled = true;
        Entity_Player.Instance.enabled = true;
        inputField.gameObject.SetActive(false);
        doneCommands.gameObject.SetActive(false);
        isActive = false;
    }
    
    public void DoCommandInput(CommandType type)
    {
        playerCommandInvoker.DoCommand(playerCommandInvoker.commandDic[type]);
    }
    private void UnDoCommandInput(CommandType type)
    {
        playerCommandInvoker.UnDoCommand(playerCommandInvoker.commandDic[type]);
    }

    public void CheckCommandPrompt()
    {
        _isValidCommand = "";
        _inputCommand = inputField.text.ToUpper();
        _inputCommand = _inputCommand.Replace(" ", "_");

        switch (_inputCommand)
        {
            case "GODMODE_ON":
            {
                DoCommandInput(CommandType.INVINCIBLE);
                break;
            }
            case "GODMODE_OFF":
            {
                UnDoCommandInput(CommandType.INVINCIBLE);
                break;
            }
            case "FULL_HEAL":
            {
                DoCommandInput(CommandType.FULL_HEAL);
                break;
            }
            case "INSTA_DEATH":
            {
                UnDoCommandInput(CommandType.FULL_HEAL);
                break;
            }
            case "ATTACK_SPEED_UP":
            {
                DoCommandInput(CommandType.ATTACK_SPEED);
                break;
            }
            case "ATTACK_SPEED_DOWN":
            {
                UnDoCommandInput(CommandType.ATTACK_SPEED);
                break;
            }
            case "SPECIAL_ATTACK_SPEED_UP":
            {
                DoCommandInput(CommandType.BOMMERANG_ATTACK_SPEED);
                break;
            }
            case "SPECIAL_ATTACK_SPEED_DOWN":
            {
                UnDoCommandInput(CommandType.BOMMERANG_ATTACK_SPEED);
                break;
            }
            case "BOOM_DISTANCE_UP":
            {
                DoCommandInput(CommandType.BOMMERANG_DISTANCE);

                break;
            }
            case "BOOM_DISTANCE_DOWN":
            {
                UnDoCommandInput(CommandType.BOMMERANG_DISTANCE);
                break;
            }
            case "MOVE_SPEED_UP":
            {
                DoCommandInput(CommandType.MOVE_SPEED);
                break;
            }
            case "MOVE_SPEED_DOWN":
            {
                UnDoCommandInput(CommandType.MOVE_SPEED);
                break;
            }
            case "HEALTH_UP":
            {
                DoCommandInput(CommandType.HEALTH_UP);
                break;
            }
            case "HEALTH_DOWN":
            {
                UnDoCommandInput(CommandType.HEALTH_UP);
                break;
            }
            default:
            {
                _isValidCommand = " : Command Not Valid";
                break;
            }
        }

        if (doneCommands.textInfo.lineCount > 5)
        {
            doneCommands.text = "";
        }

        doneCommands.text = "\n" + _inputCommand + _isValidCommand + doneCommands.text;
        inputField.text = "";
        inputField.Select();
    }
}