using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EnemyWave
{
    [Header("Timers")]
    [SerializeField] private bool isLoopable = false;
    [SerializeField] private float endOfWaveTime;
    [Range(1, 10)][SerializeField] private float _tickRange;
    private SequentialStopwatch _spawnerStopWatch;
    private SequentialStopwatch _nextWaveStopWatch;

    [Header("Factory")]
    [SerializeField] private AbstractEnemyFactory _enemyFactory;
    [Min(0)][SerializeField] private int lowQuantitySpawns;
    [Min(0)][SerializeField] private int highQuantitySpawns;

    [field:Header("Wave end event")]
    [field: SerializeField] public UnityEvent _waveEndEvent { get; private set; }

    private WaveController m_WaveController;

    public void Init(WaveController waveController, Action waveEndsCallback = null)
    {
        m_WaveController = waveController;
        _spawnerStopWatch = new SequentialStopwatch(GetRandomTimeInRange());
        _nextWaveStopWatch = new SequentialStopwatch(endOfWaveTime, waveEndsCallback);
    }

    /// <summary>
    /// </summary>
    /// <returns>Has reached next wave colliderActiveTime</returns>
    public bool Tick()
    {
        _spawnerStopWatch.OnUpdateTime();
        _nextWaveStopWatch.OnUpdateTime();

        int trueEnemyCount = m_WaveController.EnemyManager.CurrentlyActiveEnemies.Count + lowQuantitySpawns + highQuantitySpawns;
        if (trueEnemyCount > m_WaveController.MaximumEnemyCount)
        { return false; }

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

            if (isLoopable) { _nextWaveStopWatch.Reset(); }

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
        for (int i = 0; i < lowQuantitySpawns; i++)
        {
            _enemyFactory.CreateLowQuantityEnemy(m_WaveController.GetRandomSpawnPoint());
        }

        for (int i = 0; i < lowQuantitySpawns; i++)
        {
            _enemyFactory.CreateHighQuantityEnemy(m_WaveController.GetRandomSpawnPoint());
        }
    }
}
