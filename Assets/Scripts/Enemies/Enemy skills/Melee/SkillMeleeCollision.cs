using UnityEngine;

public class SkillMeleeCollision : BaseCollisionHandler
{
    [SerializeField] protected int m_damage;

    protected override void OnEntityCollisionEnter(Collision2D collision)
    {
        base.OnEntityCollisionEnter(collision);
        if (!IsValidForInteract(collision.gameObject.layer, collision.gameObject.tag)) { return; }

        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.Hit(m_damage);
        }
    }
}
