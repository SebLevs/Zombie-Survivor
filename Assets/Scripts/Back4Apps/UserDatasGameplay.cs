public class UserDatasGameplay
{
    public string objectId;
    public bool hasCompletedTutorial;
    public float MaxHealth;
    public float MoveSpeed;
    public float AttackSpeed;
    public float BoomDistance;
    public float BoomAttackSpeed;
    public float DodgeDelay;
    public int BigGold;
    public int SmallGold;

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
