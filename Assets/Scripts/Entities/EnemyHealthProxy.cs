using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealthProxy : Health
{
    [SerializeField] private Enemy proxy;

    public void OnDeathProxySpawn()
    {
        // spawn stuff here
        OnDeathProxyHealToMaxClamped();
    }

    public void OnDeathProxyHealToMaxClamped()
    {
        // clamp heal to this.max hp or proxy health if lower than this.max hp
    }
}
