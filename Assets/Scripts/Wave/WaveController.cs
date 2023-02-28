using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour, IFrameUpdateListener
{
    [field:Header("Maximum enemy count allowed")]
    [field:SerializeField] public int MaximumEnemyCount { get; private set; }

    private int _currentWaveIndex = 0;
    [SerializeField] private EnemyWave[] m_waves;
    [SerializeField] private EnemyWave m_bossWave;

    // TODO: Make generic observer pattern to call a random position from this list
    [SerializeField] private List<PositionGetter2D> _enemySpawnPoints;

    public EnemyManager EnemyManager { get; private set; }

    public EnemyWave GetCurrentWave() => m_waves[_currentWaveIndex];
    public void SetCurrentWave(int wave)
    {
        int trueWave = (wave >= m_waves.Length) ? m_waves.Length - 1 : wave;
        _currentWaveIndex = trueWave;
    }
    
    public void SetCurrentWaveAsLastWave() => SetCurrentWave(m_waves.Length - 1);

    private void Awake()
    {
        TrySetSpawnPointAsPlayer();
        SetEnemyWaves();
    }

    private void Start()
    {
        EnemyManager = EnemyManager.Instance;
    }

    public void OnUpdate()
    {
        if (_currentWaveIndex < m_waves.Length)
        {
            m_waves[_currentWaveIndex].Tick();
        }
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public bool TrySetSpawnPointAsPlayer()
    {
        PositionGetter2D playerPositionGetter2D = Entity_Player.Instance?.GetComponent<PositionGetter2D>();
        if (!playerPositionGetter2D) { return false; }

        _enemySpawnPoints.Add(playerPositionGetter2D);
        return true;
    }

    public Vector2 GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _enemySpawnPoints.Count);

        return _enemySpawnPoints[index].GetRandomPosition();
    }

    private void SetEnemyWaves()
    {
        for (int i = 0; i < m_waves.Length; i++)
        {
            if (i >= m_waves.Length -1)
            {
                m_waves[i].Init(this);
            }
            else
            {
                m_waves[i].Init(this, waveEndsCallback: () => _currentWaveIndex++);
            }
        }
    }
}
