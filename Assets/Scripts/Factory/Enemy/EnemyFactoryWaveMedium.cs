using System;
using UnityEngine;

[Serializable]
public class EnemyFactoryWaveMedium : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy()
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(Vector3.zero, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy()
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(Vector3.zero, Quaternion.identity);
    }
}
