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

    [Header("Enemy spawning timer")]
    [Range(1, 10)] [SerializeField] private float _maxWaitBetweenSpawns;
    [SerializeField] private float _spawnTimer = 5f;
    [SerializeField] private SequentialStopwatch _spawnerStopWatch;

    [Header("Enemy spawning points")]
    [SerializeField] private float _maxSpawnPointLinearRange;
    [SerializeField] private Vector2 _spawnLeft;
    [SerializeField] private Vector2 _spawnRight;
    [SerializeField] private Vector2 _spawnTop;
    [SerializeField] private Vector2 _spawnBottom;

    protected override void OnAwake()
    {
        _spawnerStopWatch = new SequentialStopwatch(GetRandomTimeInRange());
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
        return Random.Range(0, _maxWaitBetweenSpawns);
    }

    private void CreateEnemyWave()
    {
        // TODO: Set enemy placement here
        _factory.CurrentFactory.CreateLowQuantityEnemy(Vector3.zero);
        _factory.CurrentFactory.CreateHighQuantityEnemy(Vector3.zero);
    }

    private Vector2 GetRandomSpawnPoint()
    {
        // TODO: Create class for spawn points: transform + isVertical + isHorizontal
        // TODO: Create array of spawn points + Get random from array + get spawn location from spawn point
        return Vector2.zero;
    }
}
