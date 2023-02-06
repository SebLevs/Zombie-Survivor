using System;
using UnityEngine;

[Serializable]
public class EnemyFactoryWaveEasy : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy()
    {
        return EnemyManager.Instance.Larvae.GetFromAvailable(Vector3.zero, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy()
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(Vector3.zero, Quaternion.identity);
    }
}
