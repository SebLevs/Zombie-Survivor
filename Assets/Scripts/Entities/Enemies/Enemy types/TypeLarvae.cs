public class TypeLarvae : EnemyType
{
    public override void ReturnToPool()
    {
        EnemyManager.Instance.Larvae.ReturnToAvailable(m_context);
    }
}
