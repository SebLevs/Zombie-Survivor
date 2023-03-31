using UnityEngine;

public class PlayerCommand_TeleportToBoss : ICommand
{
    public PlayerCommand_TeleportToBoss()
    {
    }

    public void Execute()
    {
        Entity_Player.Instance.transform.position = PortalManager.Instance.currentActivePortal.transform.position;
    }

    public void UnExecute()
    {
        Entity_Player.Instance.transform.position = Vector3.zero;
    }
}
