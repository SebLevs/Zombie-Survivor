using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : Manager<EnemyFactoryManager>
{
    [field: SerializeField] public AbstractEnemyFactory EasyEnemyFactory { get; private set; }
    [field: SerializeField] public AbstractEnemyFactory MediumEnemyFactory { get; private set; }
    [field: SerializeField] public AbstractEnemyFactory HardEnemyFactory { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        //InitFactories();
    }

    /*private void InitFactories()
    {
        EasyEnemyFactory = new EnemyFactoryWaveEasy();
        MediumEnemyFactory = new EnemyFactoryWaveMedium();
        HardEnemyFactory = new EnemyFactoryWaveHard();
    }*/
}
