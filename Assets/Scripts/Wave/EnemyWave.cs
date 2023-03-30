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

    private IWaveController m_WaveController;
    private UIManager uiManager;

    public void Init(IWaveController waveController, Action waveEndsCallback = null)
    {
        m_WaveController = waveController;
        _spawnerStopWatch = new SequentialStopwatch(GetRandomTimeInRange());
        _nextWaveStopWatch = new SequentialStopwatch(endOfWaveTime, waveEndsCallback);
        uiManager = UIManager.Instance;
    }

    public void OnStartWave()
    {
        uiManager.ViewWaveStats.WaveElement.Element.text = _enemyFactory.FactoryName;
        UpdateViewWaveStatsVisuals();
    }

    private bool _hasStarted = false;

    /// <returns>Has reached next wave colliderActiveTime</returns>
    public bool Tick()
    {
        // TODO: ugly boolean logic: If time, refactor whole system into one that allows start and end events easily
        if (!_hasStarted) { OnStartWave(); _hasStarted = true; } 

        _spawnerStopWatch.OnUpdateTime();
        _nextWaveStopWatch.OnUpdateTime();

        UpdateViewWaveStatsVisuals();

        int trueEnemyCount = EnemyManager.Instance.CurrentlyActiveEnemies.Count + lowQuantitySpawns + highQuantitySpawns;
        if (trueEnemyCount > m_WaveController.GetMaximumEnemyCount())
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

    private void UpdateViewWaveStatsVisuals()
    {
        uiManager.ViewWaveStats.PrintTimer(_nextWaveStopWatch.CurrentTime);
        uiManager.ViewWaveStats.TimerViewElement.Filler.SetFilling(_nextWaveStopWatch.GetNormal);
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

    public void EndWave()
    {
        _spawnerStopWatch.SetNewTarget(0);
    }
}
