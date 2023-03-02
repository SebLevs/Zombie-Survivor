using UnityEngine;

public abstract class AbstractEnemyFactory :ScriptableObject
{
    [field:SerializeField] public string FactoryName { get; private set; }

    public abstract Enemy CreateLowQuantityEnemy(UnityEngine.Vector3 position);

    public abstract Enemy CreateHighQuantityEnemy(UnityEngine.Vector3 position);
}