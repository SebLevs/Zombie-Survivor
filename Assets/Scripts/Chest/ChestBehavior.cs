using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using TMP_Text = TMPro.TMP_Text;

public class ChestBehavior : MonoBehaviour, IFrameUpdateListener
{
    public int minValue;
    public int maxValue;

    private int _nextPowerUpID;
    public int chestValue;
    private Entity_Player _player;
    private Collider2D _col;

    private CommandInvoker _commandInvoker;

    private TMP_Text _uiValue;

    private Animator _anim;

    private bool _canOpenChest;

    private UIManager uiManager;

    private bool isInteractable => _player.currentGold >= chestValue;


    private void Awake()
    {
        chestValue = Random.Range(minValue, maxValue + 1);
        _uiValue = GetComponentInChildren<TMP_Text>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _player = Entity_Player.Instance;
        _commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
        _uiValue.text = "$ " + chestValue;
        uiManager = UIManager.Instance;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_player.Health.IsDead)
        {
            if (_canOpenChest == false)
            {
                _canOpenChest = true;
            }

            if (!isInteractable || uiManager.ViewInteract.gameObject.activeSelf) { return; }
            uiManager.ViewInteract.OnShow();
            uiManager.ViewInteract.Init(transform, (Vector2)_uiValue.transform.position + Vector2.right);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _canOpenChest = false;
        if (other.CompareTag("Player") && !_player.Health.IsDead)
        {
            uiManager.ViewInteract.DeactivateAndHide(transform);
        }
    }

    private void TryOpenChest()
    {
        if (isInteractable)
        {
            _anim.Play("OpenChest");
            _player.currentGold -= chestValue;

            if (_commandInvoker.ChestPowerUpDic.Count >= 1)
            {
                _nextPowerUpID = Random.Range(0 , _commandInvoker.ChestPowerUpDic.Count);
                (CommandType type, ICommand command) = _commandInvoker.ChestPowerUpDic.ElementAt(_nextPowerUpID);
                string name = type.ToString().Replace("_", " ");
                UIManager.Instance.ViewPlayerStats.ChestBonusPopup.PrintChestBonus("Bonus gained!\n" + name);
                _commandInvoker.DoCommand(command);
            }
            else
            {
                UIManager.Instance.ViewPlayerStats.ChestBonusPopup.PrintChestBonus("No more upgrades...");
            }
            
            _player.RefreshPlayerStats();
            _col.enabled = false;
            _uiValue.enabled = false;
            _player.RefreshGoldBar();
            _player.RefreshHealthBar();
        }
    }

    public void OnUpdate()
    {
        if (_player.DesiredActions.Contains(PlayerActionsType.INTERACT) && _canOpenChest)
        {
            _player.DesiredActions.ConsumeAllActions(PlayerActionsType.INTERACT);
            _canOpenChest = false;
            TryOpenChest();
        }
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
