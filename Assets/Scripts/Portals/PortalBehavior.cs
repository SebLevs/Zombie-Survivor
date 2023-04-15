using TMPro;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private int GoldNeeded = 300;
    public bool HasBossStarted => _collider.enabled == false;
    public bool IsInteractable => _player.currentGold >= GoldNeeded && _collider.enabled == true;
    private Entity_Player _player;
    [SerializeField] private PortalManager _portalManager;
    private Collider2D _collider;
    private TMP_Text _text;
    private SpriteRenderer _visuals;

    private void Awake()
    {
        // TODO: Refactor outside of Awake to avoid concurent get set of singleton conflicts
        _player = Entity_Player.Instance;
        _portalManager = PortalManager.Instance;
        _collider = GetComponent<Collider2D>();
        _text = GetComponentInChildren<TMP_Text>();
        _visuals = GetComponentInChildren<SpriteRenderer>();
        _portalManager.allPortals.Add(this);
        DeactivatePortalOnAwake();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (IsInteractable)
            {
                _portalManager.ActivateBossLevelBoundaries();
                EnemyManager enemyManager = EnemyManager.Instance;
                enemyManager.KillAllCurrentlyActiveEnemies();
                Transform target = enemyManager.ZombieBoss.GetFromAvailable(bossSpawnPoint.position, bossSpawnPoint.rotation).transform;
                _player.arrow.SetTargetAs(target);
                enemyManager.WaveController.SetCurrentWaveAsLastWave();
                DeactivatePortalOnBossSpawn();
            }
        }
    }

    public Transform GetBossSpawnPoint() => bossSpawnPoint;

    private void DeactivatePortalOnBossSpawn()
    {
        _collider.enabled = false;
        _text.gameObject.SetActive(false);
    }

    private void DeactivatePortalOnAwake()
    {
        _visuals.enabled = false;
        DeactivatePortalOnBossSpawn();
    }

    public void ActivatePortal()
    {
        _collider.enabled = true;
        _text.gameObject.SetActive(true);
        _visuals.enabled = true;
    }
}
