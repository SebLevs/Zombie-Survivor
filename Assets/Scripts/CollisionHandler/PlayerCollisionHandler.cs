using UnityEngine;

public class PlayerCollisionHandler : BaseCollisionHandler
{
    protected override void OnEntityTriggerEnter(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            //GetComponent<Health>().Hit(collision.GetComponent<Enemy>().damage);
        }
    }
}
