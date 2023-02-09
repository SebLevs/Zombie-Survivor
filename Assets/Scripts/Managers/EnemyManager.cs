using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager<EnemyManager>
{
    [field:SerializeField] public Dictionary<Enemy, Enemy> CurrentlyActiveEnemies { get; private set; }

    [field:Header("Melee")]
    [field:SerializeField] public PoolPattern<Enemy> Zombies { get; private set; }
    [field: SerializeField] public PoolPattern<Enemy> Larvae { get; private set; }

    [field: Header("Ranged")]
    [field: SerializeField] public PoolPattern<Enemy> Skeletons { get; private set; }

    [field: Header("Boss")]
    [field: SerializeField] public PoolPattern<Enemy> Boss { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        InitPools();
        CurrentlyActiveEnemies = new Dictionary<Enemy, Enemy>();
    }

    private void InitPools()
    {
        // Melee
        Larvae.InitDefaultQuantity();
        Zombies.InitDefaultQuantity();
        
        // Ranged
        Skeletons.InitDefaultQuantity();

        // Boss
        Boss.InitDefaultQuantity();
    }
}
