using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    public IProjectileStrategy strategy;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    public Rigidbody2D GetRB()
    {
        return rb;
    }
    public Collider2D GetCol()
    {
        return col;
    }
    private void Start()
    {
        strategy = Instantiate(strategy);
    }
    public void OnGetFromAvailable()
    {
        strategy.OnGetFromAvailable();
    }

    public void OnReturnToAvailable()
    {
        strategy.OnReturnToAvailable();
    }
    public void ShootBullet(Vector2 direction, float speed)
    {
        transform.up = direction;
        rb.velocity = direction * speed;
    }


}
