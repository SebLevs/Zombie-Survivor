using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : Manager<EnemyFactoryManager>
{
    [field: SerializeField] public AbstractEnemyFactory EasyEnemyWave { get; private set; }
    [field: SerializeField] public AbstractEnemyFactory MediumEnemyWave { get; private set; }
    [field: SerializeField] public AbstractEnemyFactory HardEnemyWave { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        EasyEnemyWave = new EnemyFactoryWaveEasy();
    }
}
