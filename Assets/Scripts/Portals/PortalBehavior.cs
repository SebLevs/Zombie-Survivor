using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField] Transform bossSpawnPoint;
    private const int GoldNeeded = 300;
    private Entity_Player _player;
    [SerializeField] private PortalManager _portalManager;
    private Collider2D _collider;
    private TMP_Text _text;
    private SpriteRenderer _visuals;

    private void Awake()
    {
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
            if (_player.currentGold >= GoldNeeded)
            {
                _portalManager.ActivateBossLevelBoundaries();
                DeactivatePortalOnBossSpawn();
            }
        }
    }

    public Transform GetBossSpawnPoint()
    {
        return bossSpawnPoint;
    }

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