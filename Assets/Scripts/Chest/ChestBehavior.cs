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
    private Entity_Player _player => Entity_Player.Instance;
    private Collider2D _col;

    private CommandInvoker _commandInvoker;

    private TMP_Text _uiValue;

    private Animator _anim;



    private void Awake()
    {
        chestValue = Random.Range(minValue, maxValue + 1);
        _uiValue = GetComponentInChildren<TMP_Text>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
        _uiValue.text = "$ " + chestValue;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
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
            //player.RefreshExperienceBar();
            _nextPowerUpID = Random.Range(2, _commandInvoker.commandDic.Count);

            (string name, ICommand command) = _commandInvoker.commandDic.ElementAt(_nextPowerUpID);
            _commandInvoker.DoCommand(command);
            Debug.Log(name);
            _player.RefreshPlayerStats();
            _col.enabled = false;
            _uiValue.enabled = false;
        }
    }
}
