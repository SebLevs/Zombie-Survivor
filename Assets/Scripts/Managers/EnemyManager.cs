using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager<EnemyManager>
{
    public List<Enemy> CurrentlyActiveEnemies = new List<Enemy>();

    [field:SerializeField] public PoolPattern<Enemy> EasyEnemies { get; private set; }
    [field: SerializeField] public PoolPattern<Enemy> HardEnemies { get; private set; }
    [field: SerializeField] public Enemy Boss { get; private set; }
}
