using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private int _currentWaveIndex = 0;
    [SerializeField] private EnemyWave[] m_waves;

    // TODO: Make generic observer pattern to call a random position from this list
    [SerializeField] private List<SpawnPoint2D> _enemySpawnPoints;

    private void Awake()
    {
        SetEnemyWaves();
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

    private void SetEnemyWaves()
    {
        for (int i = 0; i < m_waves.Length; i++)
        {
            if (i == m_waves.Length - 1)
            {
                m_waves[i].Init(this, callback: () =>
                {
                    // TODO: Set boss placement here
                    EnemyManager.Instance.Boss.GetFromAvailable(Vector3.zero, Quaternion.identity);
                    _currentWaveIndex++;
                });
                return;
            }

            m_waves[i].Init(this, callback: () =>
            {
                _currentWaveIndex++;
            });
        }
    }
}
