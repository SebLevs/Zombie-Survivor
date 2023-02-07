using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager<EnemyManager>
{
    public List<Enemy> CurrentlyActiveEnemies = new List<Enemy>();

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
