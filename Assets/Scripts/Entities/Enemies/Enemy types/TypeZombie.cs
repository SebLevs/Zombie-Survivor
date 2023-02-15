public class TypeZombie: EnemyType
{
    public override void ReturnToPool(Enemy key)
    {
        EnemyManager.Instance.Zombies.ReturnToAvailable(key);
    }
}
