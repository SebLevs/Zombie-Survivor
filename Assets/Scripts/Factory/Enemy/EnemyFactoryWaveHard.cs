using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Factories/Enemy Factory/Hard", fileName = "Hard enemy factory")]
public class EnemyFactoryWaveHard : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Skeletons.GetFromAvailable(position, Quaternion.identity);
    }
}
