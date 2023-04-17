public class PlayerCommand_HealthUp : ICommand
{
    public PlayerCommand_HealthUp()
    {
    }

    public void Execute()
    {
        Entity_Player player = Entity_Player.Instance;
        player.Health.SetMaxHP(Entity_Player.Instance.Health.MaxHP + 50);
        player.Health.SetCurrentHP(player.Health.CurrentHP += 50);
        Entity_Player.Instance.RefreshHealthBar();
        if (player.Health.MaxHP >= 1000)
        {
            CommandPromptManager.Instance.playerCommandInvoker.ChestPowerUpDic.Remove(CommandType.HEALTH_UP);

        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.Health.MaxHP >= 300)
        {
            Entity_Player.Instance.Health.SetMaxHP(Entity_Player.Instance.Health.MaxHP - 50);
            Entity_Player.Instance.RefreshHealthBar();
        }
    }
}