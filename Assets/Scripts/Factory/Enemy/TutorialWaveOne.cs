using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Factories/Enemy Factory/Tutorial/Wave one", fileName = "Tutorial Wave One Factory")]
public class TutorialWaveOne : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Zombies.GetFromAvailable(position, Quaternion.identity);
    }
}
