public class TypeZombieBoss : EnemyType
{
    public override void ReturnToPool()
    {
        EnemyManager.Instance.ZombieBoss.ReturnToAvailable(m_context);
    }
}
