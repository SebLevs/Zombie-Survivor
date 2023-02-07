using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehavior : MonoBehaviour, IPoolable
{
    public bool playerIsShooting = true;
    public Rigidbody2D rb;
    public CapsuleCollider2D col;
    [SerializeField] private SequentialStopwatch destroyStopWatch;
    [SerializeField] private float timeToDestroy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        timeToDestroy = 2.0f;
        destroyStopWatch = new SequentialStopwatch(timeToDestroy);
    }
    public void OnGetFromAvailable()
    {
        if(playerIsShooting)
        {
            Physics2D.IgnoreCollision(col, Entity_Player.Instance.col);
        }
    }

    public void OnReturnToAvailable()
    {
        rb.velocity = Vector2.zero;
    }


    private void OnEnable()
    {
        destroyStopWatch.Reset(false);
        destroyStopWatch.StartTimer();
        
    }

    private void Update()
    {
        destroyStopWatch.OnUpdateTime();
        if(destroyStopWatch.HasReachedTarget()) 
        {
            WeaponManager.Instance.bulletPool.ReturnToAvailable(this);
        }
    }
    
    public void ShootBullet(Vector2 direction, float speed)
    {
        transform.up = direction;
        rb.velocity = direction * speed;
    }


    

}
