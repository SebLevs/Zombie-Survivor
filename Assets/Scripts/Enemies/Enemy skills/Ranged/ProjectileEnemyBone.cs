using UnityEngine;

public class ProjectileEnemyBone : ProjectileEnemy
{
    [SerializeField] private float _rotationModifier = 1000f;
    private float _rotationZ = 0;

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
        if (IsValidForInteract(collision.gameObject.layer, collision.gameObject.tag))
        {
            Health health = collision.GetComponent<Health>();
            if (health)
            {
                health.Hit(m_damage);
                ReturnToPool();
            }
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        _rotationZ += Time.deltaTime * _rotationModifier;
        transform.rotation = Quaternion.Euler(0, 0, _rotationZ);
    }

    public override void OnReturnToAvailable()
    {
        base.OnReturnToAvailable();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
