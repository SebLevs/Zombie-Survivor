public class TypeZombie: EnemyType
{
    public override void ReturnToPool()
    {
        EnemyManager.Instance.Zombies.ReturnToAvailable(GetComponent<Enemy>());
    }
}
