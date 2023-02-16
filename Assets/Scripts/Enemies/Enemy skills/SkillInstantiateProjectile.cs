using UnityEngine;

public class SkillInstantiateProjectile : MonoBehaviour// : BaseCollisionHandler
{
    //[SerializeField] private int m_damage;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private GameObject _projectileReference;

/*    protected override void OnEntityTriggerEnter(Collider2D collision)
    {
        base.OnEntityTriggerEnter(collision);
        if (EvaluateLayers(collision.gameObject.layer, _targetMask))
        {
            Health health = collision.GetComponent<Health>();
            if (health)
            {
                health.Hit(m_damage);
            }
        }
    }*/
}
