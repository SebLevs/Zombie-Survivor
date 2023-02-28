public class PlayerCommand_DodgeDelayDown : ICommand
{
    public PlayerCommand_DodgeDelayDown()
    {
    }

    public void Execute()
    {
        Entity_Player.Instance.DodgeInterval -= 1f;
        if (Entity_Player.Instance.DodgeInterval <= 2)
        {
            CommandPromptManager.Instance.playerCommandInvoker.ChestPowerUpDic.Remove(CommandType.ATTACK_SPEED);
        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.DodgeInterval <= 8)
            Entity_Player.Instance.DodgeInterval += 1f;
    }
}
