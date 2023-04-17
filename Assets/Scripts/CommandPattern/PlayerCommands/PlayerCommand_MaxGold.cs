public class PlayerCommand_MaxGold : ICommand
{
    public PlayerCommand_MaxGold(){}
    public void Execute()
    {
        Entity_Player.Instance.currentGold = Entity_Player.Instance.MaxGold;
        Entity_Player.Instance.RefreshGoldBar();
    }

    public void UnExecute()
    {
        Entity_Player.Instance.currentGold = 0;
        Entity_Player.Instance.RefreshGoldBar();
    }
}
