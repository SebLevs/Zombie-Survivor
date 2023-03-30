using UnityEngine;

public class PracticeRoomController : MonoBehaviour
{
    public void HideWaveView() => UIManager.Instance.ViewWaveStats.OnHide();

    private void OnEnable()
    {
        Entity_Player.Instance.Health.isPermaInvincible = true;
    }

    private void OnDisable()
    {
        Entity_Player player = Entity_Player.Instance;
        if (player) 
        {
            player.Health.isPermaInvincible = false;
        }
    }
}
