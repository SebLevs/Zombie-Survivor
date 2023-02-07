using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : Manager<EnemyFactoryManager>
{
    public AbstractEnemyFactory CurrentFactory { get; set; }

    [field: SerializeField] public AbstractEnemyFactory EasyEnemyWave { get; private set; }
    [field: SerializeField] public AbstractEnemyFactory MediumEnemyWave { get; private set; }
    [field: SerializeField] public AbstractEnemyFactory HardEnemyWave { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        InitFactories();
        CurrentFactory = EasyEnemyWave;
    }

    private void InitFactories()
    {
        EasyEnemyWave = new EnemyFactoryWaveEasy();
        MediumEnemyWave = new EnemyFactoryWaveMedium();
        HardEnemyWave = new EnemyFactoryWaveHard();
    }
}
