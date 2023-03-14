using UnityEngine;

public abstract class AbstractEnemyFactory :ScriptableObject
{
    [field:SerializeField] public string FactoryName { get; private set; }

    public abstract Enemy CreateLowQuantityEnemy(Vector3 position);

    public abstract Enemy CreateHighQuantityEnemy(Vector3 position);
}
