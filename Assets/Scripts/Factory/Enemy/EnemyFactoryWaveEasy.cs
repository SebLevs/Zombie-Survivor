using System;
using UnityEngine;

[Serializable]
public class EnemyFactoryWaveEasy : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Larvae.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }
}
