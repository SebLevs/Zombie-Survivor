public class TypeBoss : EnemyType
{
    public override void ReturnToPool(Enemy key)
    {
        EnemyManager.Instance.Boss.ReturnToAvailable(key);
    }
}
