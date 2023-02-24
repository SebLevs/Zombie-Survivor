using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Factories/Enemy Factory/ZombieBoss", fileName = "ZombieBoss enemy factory")]
public class EnemyFactoryWaveBoss : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        //return EnemyManager.Instance.ZombieBoss.GetFromAvailable(position, Quaternion.identity);
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Skeletons.GetFromAvailable(position, Quaternion.identity);
    }
}
