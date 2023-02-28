using System;

[Serializable]
public class PlayerCommand_AttackSpeedUp : ICommand
{
    public PlayerCommand_AttackSpeedUp()
    {
    }

    public void Execute()
    {
        Entity_Player.Instance.attackSpeed -= 0.1f;
        if (Entity_Player.Instance.attackSpeed <= 0.1f)
        {
            CommandPromptManager.Instance.playerCommandInvoker.ChestPowerUpDic.Remove(CommandType.ATTACK_SPEED);
        }
    }

    public void UnExecute()
    {
        if (Entity_Player.Instance.attackSpeed + 0.1f < 3f)
        {
            Entity_Player.Instance.attackSpeed += 0.1f;
        }
    }
}