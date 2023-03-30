using System.Collections.Generic;
using UnityEngine;

public class TutorialWaveController : MonoBehaviour, IWaveController, IUpdateListener
{
    [Header("Waves")]
    [SerializeField] private int maximumEnemyCount;
    [SerializeField] private EnemyWave firstWave;
    [SerializeField] private EnemyWave secondWave;
    [SerializeField] private EnemyWave bossWave;
    private EnemyWave _currentWave;

    [Header("Spawn Points")]
    [SerializeField] private List<PositionGetter2D> _enemySpawnPoints;

    public EnemyManager EnemyManager { get; private set; }

    private void EndCurrentWave() => _currentWave.EndWave();

    private void Awake()
    {
        SetEnemyWaves();
    }

    private void Start()
    {
        EnemyManager = EnemyManager.Instance;
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

    public void OnUpdate()
    {
        _currentWave.Tick();
    }

    public int GetMaximumEnemyCount() { return maximumEnemyCount; }

    public Vector2 GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _enemySpawnPoints.Count);

        return _enemySpawnPoints[index].GetRandomPosition();
    }

    private void SetEnemyWaves()
    {
        bossWave.Init(this);

        secondWave.Init(this, waveEndsCallback: () =>
        {
            _currentWave = bossWave;
            EnemyManager.Instance.KillAllCurrentlyActiveEnemies();
        });

        firstWave.Init(this, waveEndsCallback: () => _currentWave = secondWave);

        _currentWave = firstWave;
    }
}
