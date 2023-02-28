public class PlayerCommand_DodgeDelayDown : ICommand
{
    public PlayerCommand_DodgeDelayDown()
    {
    }

    public void Execute()
    {
        if (Entity_Player.Instance.DodgeInterval >= 2)
            Entity_Player.Instance.DodgeInterval -= 0.5f;
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.DodgeInterval <= 8)
            Entity_Player.Instance.DodgeInterval += 0.5f;
    }
}
