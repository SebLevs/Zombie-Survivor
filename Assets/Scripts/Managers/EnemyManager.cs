using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager<EnemyManager>
{
    [field:SerializeField] public HashSet<Enemy> CurrentlyActiveEnemies { get; private set; }

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
        CurrentlyActiveEnemies = new HashSet<Enemy>();
    }

    private void InitPools()
    {
        // Melee
        Dictionary<Enemy, Enemy> larvaeDictionary = Larvae.InitDefaultQuantity();
        foreach (var item in larvaeDictionary.Values)
        {
            item.PoolRef = Larvae;
        }

        Dictionary<Enemy, Enemy> zombiesDictionary = Zombies.InitDefaultQuantity();
        foreach (var item in zombiesDictionary.Values)
        {
            item.PoolRef = Zombies;
        }

        // Ranged
        Dictionary<Enemy, Enemy> skeletonsDictionary = Skeletons.InitDefaultQuantity();
        foreach (var item in skeletonsDictionary.Values)
        {
            item.PoolRef = Skeletons;
        }

        // Boss
        Dictionary<Enemy, Enemy> bossDictionary = Boss.InitDefaultQuantity();
        foreach (var item in bossDictionary.Values)
        {
            item.PoolRef = Boss;
        }
    }

    [ContextMenu("Kill all currently active enemies")]
    public void KillAllCurrentlyActiveEnemies()
    {
        foreach (Enemy enemy in CurrentlyActiveEnemies)
        {
            enemy.Kill();
        }
    }
}
