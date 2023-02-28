using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    private Entity_Player _player;
    private CommandInvoker _commandInvoker;
    [SerializeField] private AudioElement pickupSound;

    private void Start()
    {
        _player = Entity_Player.Instance;
        _commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_player.Health.IsDead)
        {
            if (_player.Health.CurrentHP != _player.Health.MaxHP)
            {
                _commandInvoker.DoCommand(_commandInvoker.commandDic[CommandType.FULL_HEAL]);
                _player.RefreshHealthBar();
                pickupSound.PlayRandom();
                gameObject.SetActive(false);

            }
        }
    }
}
