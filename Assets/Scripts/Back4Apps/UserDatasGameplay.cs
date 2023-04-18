public class UserDatasGameplay
{
    public string objectId;
    public bool hasCompletedTutorial;
    public int MaxHealth;
    public float MoveSpeed;
    public float AttackSpeed;
    public float BoomDistance;
    public float BoomAttackSpeed;
    public float DodgeDelay;
    public int BigGold;
    public int SmallGold;

    public void SetUserDataGameplay(int maxHP, float moveSpeed, float attackSpeed,
        float boomDistance, float boomAttackSpeed, float dodgeDelay, int bigGold, int smallGold)
    {
        MaxHealth = maxHP;
        MoveSpeed = moveSpeed;
        AttackSpeed = attackSpeed;
        BoomDistance = boomDistance;
        BoomAttackSpeed = boomAttackSpeed;
        DodgeDelay = dodgeDelay;
        BigGold = bigGold;
        SmallGold = smallGold;
    }

    public override string ToString()
    {
        return $"hasCompletedTutorial : {hasCompletedTutorial}" +
               $"BoomDistance : {BoomDistance}" +
               $"MaxHealth : {MaxHealth}" +
               $"BigGold : {BigGold}" +
               $"SmallGold : {SmallGold}" +
               $"DodgeDelay : {DodgeDelay}" +
               $"BoomAttackSpeed : {BoomAttackSpeed}" +
               $"MoveSpeed : {MoveSpeed}" +
               $"AttackSpeed : {AttackSpeed}";
    }
}