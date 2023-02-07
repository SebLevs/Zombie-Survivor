using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Manager<WaveManager>
{
    private EnemyFactoryManager _factory;

    [Header("Difficulty timers")]
    [SerializeField] private float _waveTimerMedium = 10;
    [SerializeField] private float _waveTimerHard = 20;
    [SerializeField] private float _waveTimerBoss = 30;

    [Header("Enemy spawning")]
    [Range(1, 10)] [SerializeField] private float _tickRange;
    [SerializeField] private SequentialStopwatch _spawnerStopWatch;

    // TODO: Make generic observer pattern to call a random position from this list
    [SerializeField] private List<SpawnPoint2D> _enemySpawnPoints;

    protected override void OnAwake()
    {
        _spawnerStopWatch = new SequentialStopwatch(GetRandomTimeInRange());
        //_enemySpawnPoints = new List<SpawnPoint2D>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        _factory = EnemyFactoryManager.Instance;

        TimerManager.Instance.AddSequentialStopwatch(_waveTimerMedium, () =>
        {
            _factory.CurrentFactory = _factory.MediumEnemyWave;
        });

        TimerManager.Instance.AddSequentialStopwatch(_waveTimerHard, () =>
        {
            _factory.CurrentFactory = _factory.HardEnemyWave;
        });

        TimerManager.Instance.AddSequentialStopwatch(_waveTimerBoss, () =>
        {
            EnemyManager.Instance.CurrentlyActiveEnemies.ForEach(enemy => enemy.Kill());
            // TODO: Set enemy placement here
            EnemyManager.Instance.Boss.GetFromAvailable(Vector3.zero,Quaternion.identity);
        });
    }

    private void Update()
    {
        _spawnerStopWatch.OnUpdateTime();

        if (_spawnerStopWatch.HasReachedTarget())
        {
            _spawnerStopWatch.Reset(true);
            _spawnerStopWatch.SetNewTarget(GetRandomTimeInRange());
            CreateEnemyWave();
            _spawnerStopWatch.StartTimer();
        }
    }

    private float GetRandomTimeInRange()
    {
        return Random.Range(0, _tickRange);
    }

    private void CreateEnemyWave()
    {
        // TODO: Set enemy placement here
        _factory.CurrentFactory.CreateLowQuantityEnemy(GetRandomSpawnPoint());
        _factory.CurrentFactory.CreateHighQuantityEnemy(GetRandomSpawnPoint());
    }

    private Vector2 GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _enemySpawnPoints.Count);

        return _enemySpawnPoints[index].GetRandomSpawnPoint();
    }
}
