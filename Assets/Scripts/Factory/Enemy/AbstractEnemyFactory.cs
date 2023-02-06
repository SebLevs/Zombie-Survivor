using System;
using System.Numerics;
using UnityEngine;

[Serializable]
public abstract class AbstractEnemyFactory
{
    public abstract Enemy CreateLowQuantityEnemy(UnityEngine.Vector3 position);

    public abstract Enemy CreateHighQuantityEnemy(UnityEngine.Vector3 position);
}