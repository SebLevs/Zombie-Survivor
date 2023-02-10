using System.Collections.Generic;
using System.Linq;
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
        HashSet<Enemy> larvaeDictionary = Larvae.InitDefaultQuantity();
        foreach (var item in larvaeDictionary)
        {
            item.PoolRef = Larvae;
        }

        HashSet<Enemy> zombiesDictionary = Zombies.InitDefaultQuantity();
        foreach (var item in zombiesDictionary)
        {
            item.PoolRef = Zombies;
        }

        // Ranged
        HashSet<Enemy> skeletonsDictionary = Skeletons.InitDefaultQuantity();
        foreach (var item in skeletonsDictionary)
        {
            item.PoolRef = Skeletons;
        }

        // Boss
        HashSet<Enemy> bossDictionary = Boss.InitDefaultQuantity();
        foreach (var item in bossDictionary)
        {
            item.PoolRef = Boss;
        }
    }

    [ContextMenu("Kill all currently active enemies")]
    public void KillAllCurrentlyActiveEnemies()
    {
        for (int i = 0; i < CurrentlyActiveEnemies.Count; i++)
        {
            CurrentlyActiveEnemies.ElementAt(i).Kill();
        }
    }
}
