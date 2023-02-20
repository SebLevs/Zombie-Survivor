public class PlayerCommand_MoveSpeedUp : ICommand
{
    public PlayerCommand_MoveSpeedUp()
    {
    }

    public void Execute()
    {
        if (Entity_Player.Instance.MovSpeed < 15f)
        {
            Entity_Player.Instance.MovSpeed += 1.0f;
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
