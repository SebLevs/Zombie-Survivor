using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using TMP_Text = TMPro.TMP_Text;

public class ChestBehavior : MonoBehaviour, IUpdateListener
{
    private const string _noMoreUpgradesKey = "tmp noMoreUpgrades";
    public static List<ChestBehavior> AllChest;
    
    public int minValue;
    public int maxValue;

    private int _nextPowerUpID;
    public int chestValue;
    private Entity_Player _player;
    private CircleCollider2D _col;

    private CommandInvoker _commandInvoker;

    private TMP_Text _uiValue;

    private Animator _anim;

    public bool CanOpenChest { get; private set; }

    private UIManager uiManager;

    public bool isInteractable => _player.currentGold >= chestValue && _col.enabled == true;

    [Header("Chest Type")]
    [SerializeField] private bool isRandomBonus = true;
    [SerializeField] private CommandType specificBonusType;

    private void Awake()
    {
        chestValue = Random.Range(minValue, maxValue + 1);
        _uiValue = GetComponentInChildren<TMP_Text>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<CircleCollider2D>();
        if (AllChest == null) { AllChest = new(); }
    }

    private void Start()
    {
        _player = Entity_Player.Instance;
        _commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
        _uiValue.text = "$ " + chestValue;
        uiManager = UIManager.Instance;
        AllChest.Add(this);
        CanOpenChest = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_player.Health.IsDead)
        {
            if (CanOpenChest == false)
            {
                CanOpenChest = true;
            }

            if (!isInteractable || uiManager.ViewInteract.gameObject.activeSelf) { return; }
            uiManager.ViewInteract.OnShow();
            uiManager.ViewInteract.Init(transform, (Vector2)_uiValue.transform.position + Vector2.right);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_player.Health.IsDead)
        {
            CanOpenChest = false;
            uiManager.ViewInteract.DeactivateAndHide(transform);
        }
    }

    public void TryOpenChest()
    {
        if (isInteractable)
        {
            _col.enabled = false;
            _uiValue.enabled = false;

            _anim.Play("OpenChest");
            _player.currentGold -= chestValue;

            if (_commandInvoker.ChestPowerUpDic.Count >= 1)
            {
                _nextPowerUpID = Random.Range(0 , _commandInvoker.ChestPowerUpDic.Count);
                (CommandType type, ICommand command) = _commandInvoker.ChestPowerUpDic.ElementAt(_nextPowerUpID);
                string key = CommandPromptManager.Instance.GetLocalizationKeyForCommand(type);
                uiManager.ViewPlayerStats.ChestBonusPopup.PrintChestBonus(key);
                _commandInvoker.DoCommand(command);
            }
            else
            {
                UIManager.Instance.ViewPlayerStats.ChestBonusPopup.PrintChestBonus(_noMoreUpgradesKey);
            }

            _player.RefreshPlayerStats();
            _player.RefreshGoldBar();
            _player.RefreshHealthBar();
        }
    }

    public void CallCommand(CommandType type)
    {
        ICommand icommand;
        if (_commandInvoker.ChestPowerUpDic.TryGetValue(type, out icommand))
        {
            //ICommand icommand = _commandInvoker.ChestPowerUpDic[type];
            string key = CommandPromptManager.Instance.GetLocalizationKeyForCommand(type);
            uiManager.ViewPlayerStats.ChestBonusPopup.PrintChestBonus(key);
            _commandInvoker.DoCommand(icommand);

            _player.RefreshPlayerStats();
            _player.RefreshHealthBar();
        }
        else
        {
            uiManager.ViewPlayerStats.ChestBonusPopup.PrintChestBonus(_noMoreUpgradesKey);
        }
    }

    public void OnUpdate()
    {
        if (_player.DesiredActions.Contains(PlayerActionsType.INTERACT) && CanOpenChest)
        {
            _player.DesiredActions.ConsumeAllActions(PlayerActionsType.INTERACT);
            CanOpenChest = false;

            if (isRandomBonus)
            {
                TryOpenChest();
            }
            else
            {
                CallCommand(specificBonusType);
            }
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
