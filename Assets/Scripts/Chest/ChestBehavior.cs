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
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_player.Health.IsDead)
        {
            if (_canOpenChest == false)
            {
                _canOpenChest = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _canOpenChest = false;
    }

    private void TryOpenChest()
    {
        if (_player.currentGold >= chestValue)
        {
            _anim.Play("OpenChest");
            _player.currentGold -= chestValue;
            _nextPowerUpID = Random.Range(0 , _commandInvoker.ChestPowerUpDic.Count);
            (CommandType type, ICommand command) = _commandInvoker.ChestPowerUpDic.ElementAt(_nextPowerUpID);
            string name = type.ToString().Replace("_", " ");
            UIManager.Instance.ViewPlayerStats.ChestBonusPopup.PrintChestBonus("Bonus gained!\n" + name);
            _commandInvoker.DoCommand(command);
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
