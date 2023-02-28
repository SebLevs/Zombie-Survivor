using UnityEngine;

public class SkillMeleeTrigger : BaseCollisionHandler
{
    [SerializeField] protected int m_damage;

    protected override void OnEntityTriggerEnter(Collider2D collision)
    {
        base.OnEntityTriggerEnter(collision);

        if (!IsValidForInteract(collision.gameObject.layer, collision.gameObject.tag)) { return; }

        Health health = collision.GetComponent<Health>();
        if (health)
        {
            health.Hit(m_damage);
        }
    }
}
