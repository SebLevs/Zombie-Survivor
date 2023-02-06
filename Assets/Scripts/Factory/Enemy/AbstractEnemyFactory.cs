using System;

[Serializable]
public abstract class AbstractEnemyFactory
{
    public abstract Enemy CreateLowQuantityEnemy();

    public abstract Enemy CreateHighQuantityEnemy();
}