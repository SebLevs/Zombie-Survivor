using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletStrategy : IProjectileStrategy
{
    public bool playerIsShooting = true;
    
    [SerializeField] private SequentialStopwatch destroyStopWatch;
    [SerializeField] private float timeToDestroy;

    private void Awake()
    {
        timeToDestroy = 2.0f;
        destroyStopWatch = new SequentialStopwatch(timeToDestroy);
    }
    public override void Execute(Projectile projectile)
    {
        projectile.StopAllCoroutines();
        projectile.StartCoroutine(fakeUpdate(projectile));
    }

    public override void OnGetFromAvailable(Projectile projectile = null)
    {
        if (playerIsShooting)
        {
            Physics2D.IgnoreCollision(projectile.GetCol(), Entity_Player.Instance.col);
        }
    }

    public override void OnReturnToAvailable(Projectile projectile = null)
    {
        projectile.GetRB().velocity = Vector2.zero;
    }
    private void OnEnable()
    {
        destroyStopWatch.Reset(false);
        destroyStopWatch.StartTimer();

    }
    private IEnumerator fakeUpdate(Projectile projectile = null)
    {
        while(!destroyStopWatch.HasReachedTarget())
        {
            yield return new WaitForFixedUpdate();
            destroyStopWatch.OnUpdateTime();
        }
        //WeaponManager.Instance.bulletPool.ReturnToAvailable(projectile);
        yield return null;
    }
    
}
