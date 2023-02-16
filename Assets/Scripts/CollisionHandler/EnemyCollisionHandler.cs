using UnityEngine;

public class EnemyCollisionHandler : BaseCollisionHandler
{
    protected override void OnEntityCollisionExit(Collision2D collision)
    {
        base.OnEntityCollisionExit(collision);
        m_rigidBody.velocity = Vector3.zero;
    }
}
