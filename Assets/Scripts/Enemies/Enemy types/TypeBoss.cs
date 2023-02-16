public class TypeBoss : EnemyType
{
    public override void ReturnToPool()
    {
        EnemyManager.Instance.Boss.ReturnToAvailable(m_context);
    }
}
