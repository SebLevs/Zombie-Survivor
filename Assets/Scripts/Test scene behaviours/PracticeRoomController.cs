using UnityEngine;

public class PracticeRoomController : MonoBehaviour
{
    private void OnEnable()
    {
        Entity_Player.Instance.Health.isPermaInvincible = true;
    }

    private void OnDisable()
    {
        Entity_Player.Instance.Health.isPermaInvincible = false;
    }
}
