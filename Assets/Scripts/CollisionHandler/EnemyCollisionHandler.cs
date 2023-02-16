using UnityEngine;

public class EnemyCollisionHandler : BaseCollisionHandler
{
    protected override void OnEntityCollisionEnter(Collision2D collision)
    {
        base.OnEntityCollisionEnter(collision);
        m_rigidBody.velocity = Vector3.zero;
    }
}
