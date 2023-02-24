using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Factories/Enemy Factory/Easy", fileName = "Easy enemy factory")]
public class EnemyFactoryWaveEasy : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        //return EnemyManager.Instance.Larvae.GetFromAvailable(position, Quaternion.identity);
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }
}
