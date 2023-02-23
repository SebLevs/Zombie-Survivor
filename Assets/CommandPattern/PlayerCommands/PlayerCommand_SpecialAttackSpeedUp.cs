public class PlayerCommand_SpecialAttackSpeedUp : ICommand
{
    public PlayerCommand_SpecialAttackSpeedUp()
    {
    }

    public void Execute()
    {
        if (Entity_Player.Instance.specialAttackSpeed > 0.5f)
        {
            Entity_Player.Instance.specialAttackSpeed -= 0.1f;
        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.specialAttackSpeed < 5f)
        {
            Entity_Player.Instance.specialAttackSpeed += 0.1f;
        }
    }
}
