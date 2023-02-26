using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    private Entity_Player _player;

    private void Start()
    {
        _player = Entity_Player.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_player.Health.IsDead)
        {
            if (_player.Health.CurrentHP != _player.Health.MaxHP)
            {
                _player.Health.SetCurrentHP(_player.Health.MaxHP);
                _player.RefreshHealthBar();
            }
        }
    }
}
