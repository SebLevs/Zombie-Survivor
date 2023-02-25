using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeCollision : BaseCollisionHandler
{
    [SerializeField] protected int m_damage;

    protected override void OnEntityCollisionEnter(Collision2D collision)
    {
        base.OnEntityCollisionEnter(collision);
        if (!IsOtherLayerAlsoTargetLayer(collision.gameObject.layer, _targetMask)) { return; }

        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.Hit(m_damage);
        }
    }
}
