using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EnemyWave
{
    [Header("Timers")]
    [SerializeField] private float _endOfWaveTime;
    [Range(1, 10)][SerializeField] private float _tickRange;
    private SequentialStopwatch _spawnerStopWatch;
    private SequentialStopwatch _nextWaveStopWatch;

    [Header("Factory")]
    [SerializeField] private AbstractEnemyFactory _enemyFactory;
    [Min(0)][SerializeField] private int _lowQuantitySpawns;
    [Min(0)][SerializeField] private int _highQuantitySpawns;

    [field:Header("Wave end event")]
    [field: SerializeField] public UnityEvent _waveEndEvent { get; private set; }

    private WaveController m_waveManager;

    public void Init(WaveController waveManager, Action waveEndsCallback = null)
    {
        m_waveManager = waveManager;
        _spawnerStopWatch = new SequentialStopwatch(GetRandomTimeInRange());
        _nextWaveStopWatch = new SequentialStopwatch(_endOfWaveTime, waveEndsCallback);
    }

    /// <summary>
    /// </summary>
    /// <returns>Has reached next wave time</returns>
    public bool Tick()
    {
        _spawnerStopWatch.OnUpdateTime();
        _nextWaveStopWatch.OnUpdateTime();

        if (_spawnerStopWatch.HasReachedTarget())
        {
            _spawnerStopWatch.Reset(true);
            _spawnerStopWatch.SetNewTarget(GetRandomTimeInRange());
            CreateEnemyWave();
            _spawnerStopWatch.StartTimer();
        }

        if (_nextWaveStopWatch.HasReachedTarget())
        {
            _waveEndEvent?.Invoke();
            return true;
        }
        return false;
    }

    private float GetRandomTimeInRange()
    {
        return UnityEngine.Random.Range(0, _tickRange);
    }

    private void CreateEnemyWave()
    {
        for (int i = 0; i < _lowQuantitySpawns; i++)
        {
            _enemyFactory.CreateLowQuantityEnemy(m_waveManager.GetRandomSpawnPoint());
        }

        for (int i = 0; i < _lowQuantitySpawns; i++)
        {
            _enemyFactory.CreateHighQuantityEnemy(m_waveManager.GetRandomSpawnPoint());
        }
    }
}
