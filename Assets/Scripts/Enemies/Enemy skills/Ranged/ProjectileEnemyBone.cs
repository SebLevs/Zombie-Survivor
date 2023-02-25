using UnityEngine;

public class ProjectileEnemyBone : ProjectileEnemy
{
    public override ProjectileEnemy GetFromPool(Transform spawnLocation)
    {
        return WeaponManager.Instance.bonePool.GetFromAvailable(spawnLocation.position, spawnLocation.rotation);
    }

    public override void ReturnToPool()
    {
        WeaponManager.Instance.bonePool.ReturnToAvailable(this);
    }

    protected override void OnEntityTriggerEnter(Collider2D collision)
    {
        base.OnEntityTriggerEnter(collision);
        if (IsOtherLayerAlsoTargetLayer(collision.gameObject.layer, _targetMask))
        {
            Health health = collision.GetComponent<Health>();
            if (health)
            {
                health.Hit(m_damage);
                ReturnToPool();
            }
        }
    }
}
