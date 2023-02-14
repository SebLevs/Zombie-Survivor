public class PlayerCommand_BoomDistanceUp : ICommand
{
    public PlayerCommand_BoomDistanceUp()
    {
    }

    public void Execute()
    {
        if (Entity_Player.Instance.boomDistance < 25f)
        {
            Entity_Player.Instance.boomDistance += 1.0f;
        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.boomDistance > 15f)
        {
            Entity_Player.Instance.boomDistance -= 1.0f;
        }
    }
}
