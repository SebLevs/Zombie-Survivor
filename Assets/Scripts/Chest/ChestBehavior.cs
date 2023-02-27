using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using TMP_Text = TMPro.TMP_Text;

public class ChestBehavior : MonoBehaviour
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
    
    [SerializeField] private AudioElement openChestSound;


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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_player.Health.IsDead)
        {
            TryOpenChest();
        }
    }

    private void TryOpenChest()
    {
        if (_player.currentGold >= chestValue)
        {
            _anim.Play("OpenChest");
            _player.currentGold -= chestValue;
            _nextPowerUpID = Random.Range(2, _commandInvoker.commandDic.Count);
            (CommandType type, ICommand command) = _commandInvoker.commandDic.ElementAt(_nextPowerUpID);
            string name = type.ToString().Replace("_", " ");
            UIManager.Instance.ViewPlayerStats.ChestBonusPopup.PrintChestBonus("Bonus gained!\n" + name);
            _commandInvoker.DoCommand(command);
            openChestSound.PlayRandom();
            _player.RefreshPlayerStats();
            _col.enabled = false;
            _uiValue.enabled = false;
            _player.RefreshGoldBar();
            _player.RefreshHealthBar();
        }
    }
}
