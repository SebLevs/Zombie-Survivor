using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : Manager<EnemyManager>
{
    [field: SerializeField] public HashSet<Enemy> CurrentlyActiveEnemies { get; private set; }

    [field: Header("Melee")]
    [field: SerializeField] public PoolPattern<Enemy> Zombies { get; private set; }
    [field: SerializeField] public PoolPattern<Enemy> Larvae { get; private set; }

    [field: Header("Ranged")]
    [field: SerializeField] public PoolPattern<Enemy> Skeletons { get; private set; }

    [field: Header("ZombieBoss")]
    [field: SerializeField] public PoolPattern<Enemy> ZombieBoss { get; private set; }
    
    [field: Header("PermaGold")]
    [field: SerializeField] public PoolPattern<PermaGoldPickUp> PermaGold { get; private set; }

    public WaveController WaveController { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        InitPools();
        CurrentlyActiveEnemies = new();
    }

    protected override void OnStart()
    {
        base.OnStart();
        WaveController = GetComponentInChildren<WaveController>();
    }

    private void InitPools()
    {
        Larvae.InitDefaultQuantity();
        Zombies.InitDefaultQuantity();

        Skeletons.InitDefaultQuantity();

        ZombieBoss.InitDefaultQuantity();

        //PermaGold.InitDefaultQuantity();
    }

    [ContextMenu("Kill all currently active enemies")]
    public void KillAllCurrentlyActiveEnemies()
    {
        // Keep for reference
        // Huge lag when called: To be debugged (Permanent currency drops that instantiate at infinity one inside the other when enemy is killed?)
/*        for (int i = 0; i < CurrentlyActiveEnemies.Count; i++)
        {
            //CurrentlyActiveEnemies.ElementAt(i).OnStopAllCoroutines();
            CurrentlyActiveEnemies.ElementAt(i).Kill();
        }*/

        Collider2D[] collisions = new Collider2D[100];
        Physics2D.OverlapBoxNonAlloc(transform.position, new Vector2(500, 500), 0, collisions);

        foreach (var collision in collisions)
        {
            if (!collision) { break; }
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.ReturnToPool();
            }
        }
    }
}
