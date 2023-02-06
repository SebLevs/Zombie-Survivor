using System;
using UnityEngine;

[Serializable]
public class EnemyFactoryWaveHard : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy()
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(Vector3.zero, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy()
    {
        return EnemyManager.Instance.Skeletons.GetFromAvailable(Vector3.zero, Quaternion.identity);
    }
}
