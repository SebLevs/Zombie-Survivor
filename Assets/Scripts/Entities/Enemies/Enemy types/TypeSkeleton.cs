public class TypeSkeleton : EnemyType
{
    public override void ReturnToPool(Enemy key)
    {
        EnemyManager.Instance.Skeletons.ReturnToAvailable(key);
    }
}
