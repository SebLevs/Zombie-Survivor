using System;
using System.Numerics;
using UnityEngine;

public abstract class AbstractEnemyFactory :ScriptableObject
{
    public abstract Enemy CreateLowQuantityEnemy(UnityEngine.Vector3 position);

    public abstract Enemy CreateHighQuantityEnemy(UnityEngine.Vector3 position);
}