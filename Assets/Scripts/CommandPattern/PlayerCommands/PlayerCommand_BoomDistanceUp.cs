public class PlayerCommand_BoomDistanceUp : ICommand
{
    public PlayerCommand_BoomDistanceUp()
    {
    }

    public void Execute()
    {
        Entity_Player.Instance.boomDistance += 1.0f;
        if (Entity_Player.Instance.boomDistance >= 25f)
        {
            CommandPromptManager.Instance.playerCommandInvoker.ChestPowerUpDic.Remove(CommandType.BOMMERANG_DISTANCE);

        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.boomDistance > 5f)
        {
            Entity_Player.Instance.boomDistance -= 1.0f;
        }
    }
}