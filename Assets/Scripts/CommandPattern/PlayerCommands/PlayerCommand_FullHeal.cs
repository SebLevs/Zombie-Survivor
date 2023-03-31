public class PlayerCommand_FullHeal : ICommand
{
    public PlayerCommand_FullHeal()
    {
    }

    public void Execute()
    {
        if (Entity_Player.Instance.Health.CurrentHP <= Entity_Player.Instance.Health.MaxHP)
        {
            Entity_Player.Instance.Health.FullHeal();
            Entity_Player.Instance.RefreshHealthBar();
        }
    }


    public void UnExecute()
    {
        Entity_Player.Instance.Health.OnInstantDeath();
        Entity_Player.Instance.RefreshHealthBar();
    }
}