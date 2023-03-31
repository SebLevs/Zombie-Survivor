public class PlayerCommand_SpecialAttackSpeedUp : ICommand
{
    public PlayerCommand_SpecialAttackSpeedUp()
    {
    }

    public void Execute()
    {
        Entity_Player.Instance.specialAttackSpeed -= 0.5f;
        if (Entity_Player.Instance.specialAttackSpeed <= 0.5f)
        {
            CommandPromptManager.Instance.playerCommandInvoker.ChestPowerUpDic.Remove(CommandType.BOMMERANG_ATTACK_SPEED);

        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.specialAttackSpeed < 5f)
        {
            Entity_Player.Instance.specialAttackSpeed += 0.5f;
        }
    }
}