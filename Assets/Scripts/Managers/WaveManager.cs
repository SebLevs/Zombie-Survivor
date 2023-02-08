using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Manager<WaveManager>
{
    private EnemyFactoryManager _factory;

    private int _currentWaveIndex = 0;
    [SerializeField] private WaveController[] m_waves;

    // TODO: Make generic observer pattern to call a random position from this list
    [SerializeField] private List<SpawnPoint2D> _enemySpawnPoints;

    protected override void OnAwake()
    {
        for (int i = 0; i < m_waves.Length; i++)
        {
            if (i == m_waves.Length - 1)
            {
                m_waves[i].Init(callback: () =>
                {
                    // TODO: Set boss placement here
                    EnemyManager.Instance.Boss.GetFromAvailable(Vector3.zero, Quaternion.identity);
                    _currentWaveIndex++;
                });
                return;
            }

            m_waves[i].Init(callback: () =>
            {
                _currentWaveIndex++;
            });
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        _factory = EnemyFactoryManager.Instance;
    }

    private void Update()
    {
        if (_currentWaveIndex < m_waves.Length) 
        {
            m_waves[_currentWaveIndex].Tick();
        }
    }

    public Vector2 GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _enemySpawnPoints.Count);

        return _enemySpawnPoints[index].GetRandomSpawnPoint();
    }
}
