using System;

[Serializable]
public class PlayerCommand_Invincibility : ICommand
{
    public PlayerCommand_Invincibility()
    {
    }

    public void Execute()
    {
        if (!Entity_Player.Instance.Health.isPermaInvincible)
        {
            Entity_Player.Instance.Health.isPermaInvincible = true;
        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.Health.isPermaInvincible)
        {
            Entity_Player.Instance.Health.isPermaInvincible = false;
        }
    }
}