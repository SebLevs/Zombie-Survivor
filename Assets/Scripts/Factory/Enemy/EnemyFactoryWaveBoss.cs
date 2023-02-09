using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Factories/Enemy Factory/Boss", fileName = "Boss enemy factory")]
public class EnemyFactoryWaveBoss : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Boss.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return null;
    }
}
