using System.Linq;
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
                if (!SceneLoadManager.Instance.IsInTitleScreen && UIManager.Instance.CurrentView != UIManager.Instance.ViewOptionMenu)
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

    public void CheckCommandPrompt()
    {
        _isValidCommand = "";
        _inputCommand = inputField.text.ToUpper();
        _inputCommand = _inputCommand.Replace(" ", "_");

        switch (_inputCommand)
        {
            case "GODMODE_ON":
            {
                playerCommandInvoker.DoCommand(playerCommandInvoker.commandDic.ElementAt(0).Value);
                break;
            }
            case "GODMODE_OFF":
            {
                playerCommandInvoker.UnDoCommand(playerCommandInvoker.commandDic.ElementAt(0).Value);
                break;
            }
            case "ATTACK_SPEED_UP":
            {
                playerCommandInvoker.DoCommand(playerCommandInvoker.commandDic.ElementAt(1).Value);
                break;
            }
            case "ATTACK_SPEED_DOWN":
            {
                playerCommandInvoker.UnDoCommand(playerCommandInvoker.commandDic.ElementAt(1).Value);
                break;
            }
            case "SPECIAL_ATTACK_SPEED_UP":
            {
                playerCommandInvoker.DoCommand(playerCommandInvoker.commandDic.ElementAt(2).Value);
                break;
            }
            case "SPECIAL_ATTACK_SPEED_DOWN":
            {
                playerCommandInvoker.UnDoCommand(playerCommandInvoker.commandDic.ElementAt(2).Value);
                break;
            }
            case "BOOM_DISTANCE_UP":
            {
                playerCommandInvoker.DoCommand(playerCommandInvoker.commandDic.ElementAt(3).Value);
                break;
            }
            case "BOOM_DISTANCE_DOWN":
            {
                playerCommandInvoker.UnDoCommand(playerCommandInvoker.commandDic.ElementAt(3).Value);
                break;
            }
            case "MOVE_SPEED_UP":
            {
                playerCommandInvoker.DoCommand(playerCommandInvoker.commandDic.ElementAt(4).Value);
                break;
            }
            case "MOVE_SPEED_DOWN":
            {
                playerCommandInvoker.UnDoCommand(playerCommandInvoker.commandDic.ElementAt(4).Value);
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
