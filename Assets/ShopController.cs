using UnityEngine;

public class ShopController : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
    }

    public void TryConvertGold()
    {
        if (Entity_Player.Instance.baseStats.SmallGold >= 100)
        {
            Entity_Player.Instance.baseStats.SmallGold -= 100;
            Entity_Player.Instance.baseStats.BigGold += 1;
            UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
        }
    }

    public void IncreaseAttackSpeed()
    {
        if (Entity_Player.Instance.baseStats.BigGold <= 0 || Entity_Player.Instance.baseStats.AttackSpeed <= 0.2f) return;
        Entity_Player.Instance.baseStats.BigGold -= 1;
        CommandPromptManager.Instance.DoCommandInput(CommandType.ATTACK_SPEED);
        Entity_Player.Instance.baseStats.AttackSpeed = Entity_Player.Instance.attackSpeed;
        UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
        Entity_Player.Instance.attackSpeed = Entity_Player.Instance.baseStats.AttackSpeed;
    }
    public void IncreaseBoomAttackSpeed()
    {
        if (Entity_Player.Instance.baseStats.BigGold <= 0 || Entity_Player.Instance.baseStats.BoomAttackSpeed <= 2f) return;
        Entity_Player.Instance.baseStats.BigGold -= 1;
        CommandPromptManager.Instance.DoCommandInput(CommandType.BOMMERANG_ATTACK_SPEED);
        Entity_Player.Instance.baseStats.BoomAttackSpeed = Entity_Player.Instance.specialAttackSpeed;
        UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
        Entity_Player.Instance.specialAttackSpeed = Entity_Player.Instance.baseStats.BoomAttackSpeed;
    }
    public void IncreaseBoomDistance()
    {
        if (Entity_Player.Instance.baseStats.BigGold <= 0 || Entity_Player.Instance.baseStats.BoomDistance >= 20f) return;
        Entity_Player.Instance.baseStats.BigGold -= 1;
        CommandPromptManager.Instance.DoCommandInput(CommandType.BOMMERANG_DISTANCE);
        Entity_Player.Instance.baseStats.BoomDistance = Entity_Player.Instance.boomDistance;
        UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
        Entity_Player.Instance.boomDistance = Entity_Player.Instance.baseStats.BoomDistance;
    }
    public void IncreaseMoveSpeed()
    {
        if (Entity_Player.Instance.baseStats.BigGold <= 0 || Entity_Player.Instance.baseStats.MoveSpeed >= 15f) return;
        Entity_Player.Instance.baseStats.BigGold -= 1;
        CommandPromptManager.Instance.DoCommandInput(CommandType.MOVE_SPEED);
        Entity_Player.Instance.baseStats.MoveSpeed = Entity_Player.Instance.MovSpeed;
        UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
        Entity_Player.Instance.MovSpeed = Entity_Player.Instance.baseStats.MoveSpeed;
    }
    public void IncreaseDodgeCool()
    {
        if (Entity_Player.Instance.baseStats.BigGold <= 0 || Entity_Player.Instance.baseStats.DodgeDelay <= 2f) return;
        Entity_Player.Instance.baseStats.BigGold -= 1;
        CommandPromptManager.Instance.DoCommandInput(CommandType.DODGE_DELAY_DOWN);
        Entity_Player.Instance.baseStats.DodgeDelay = Entity_Player.Instance.dodgeInterval;
        UIManager.Instance.ViewShop.CurrenciesRefresherShorthand();
        Entity_Player.Instance.InitPlayer();
        Entity_Player.Instance.dodgeInterval = Entity_Player.Instance.baseStats.DodgeDelay;
    }
}
