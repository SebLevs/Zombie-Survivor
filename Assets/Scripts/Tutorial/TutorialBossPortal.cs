using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialBossPortal : MonoBehaviour
{
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] private int GoldNeeded = 300;
    private Entity_Player _player;
    private TMP_Text _tmpCurrencyRequired;

    [SerializeField] private Transform zombieBoss;
    [SerializeField] private List<GameObject> bossArenaBoundaries;
    [SerializeField] private GameObject visuals;

    private void Awake()
    {
        _tmpCurrencyRequired = GetComponentInChildren<TMP_Text>();
        _tmpCurrencyRequired.text = "$0";
    }

    private void Start()
    {
        _player = Entity_Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (_player.currentGold >= GoldNeeded)
            {
                ActivateBossLevelBoundaries();

                EnemyManager enemyManager = EnemyManager.Instance;
                enemyManager.KillAllCurrentlyActiveEnemies();

                _player.arrow.SetTargetAs(zombieBoss);
                zombieBoss.gameObject.SetActive(true);
                visuals.SetActive(false);
            }
        }
    }

    public void ActivateBossLevelBoundaries()
    {
        foreach (GameObject boundary in bossArenaBoundaries)
        {
            boundary.SetActive(true);
        }
    }
}
