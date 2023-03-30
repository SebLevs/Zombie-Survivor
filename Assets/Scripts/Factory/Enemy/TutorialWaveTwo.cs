using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Factories/Enemy Factory/Tutorial/Wave two", fileName = "Tutorial Wave Two Factory")]
public class TutorialWaveTwo : AbstractEnemyFactory
{
    public override Enemy CreateHighQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Skeletons.GetFromAvailable(position, Quaternion.identity);
    }

    public override Enemy CreateLowQuantityEnemy(Vector3 position)
    {
        return EnemyManager.Instance.Skeletons.GetFromAvailable(position, Quaternion.identity);
    }
}
