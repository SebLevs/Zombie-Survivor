using UnityEngine;

public class TypeZombieBoss : EnemyType
{
    [SerializeField] private AudioElement spawnSounds;

    public override void ReturnToPool()
    {
        EnemyManager.Instance.ZombieBoss.ReturnToAvailable(m_context);
    }

    private void OnEnable()
    {
        spawnSounds.PlayRandom();
    }
}
