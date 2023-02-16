public class TypeSkeleton : EnemyType
{
    public override void ReturnToPool()
    {
        EnemyManager.Instance.Skeletons.ReturnToAvailable(m_context);
    }
}
