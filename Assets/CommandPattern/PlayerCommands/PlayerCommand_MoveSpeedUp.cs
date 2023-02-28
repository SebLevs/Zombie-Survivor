public class PlayerCommand_MoveSpeedUp : ICommand
{
    public PlayerCommand_MoveSpeedUp()
    {
    }

    public void Execute()
    {
        Entity_Player.Instance.MovSpeed += 1.0f;
        if (Entity_Player.Instance.MovSpeed >= 15f)
        {
            CommandPromptManager.Instance.playerCommandInvoker.ChestPowerUpDic.Remove(CommandType.MOVE_SPEED);

        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.MovSpeed > 5f)
        {
            Entity_Player.Instance.MovSpeed -= 1.0f;
        }
    }
}