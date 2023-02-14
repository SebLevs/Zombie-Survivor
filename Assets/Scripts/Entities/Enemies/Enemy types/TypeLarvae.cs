public class TypeLarvae : EnemyType
{
    public override void ReturnToPool(Enemy key)
    {
        EnemyManager.Instance.Larvae.ReturnToAvailable(key);
    }
}
