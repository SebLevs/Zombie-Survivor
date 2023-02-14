using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    protected virtual void OnEntityCollisionEnter(Collision2D collision) { }
    protected virtual void OnEntityCollisionLeave(Collision2D collision) { }
    protected virtual void OnEntityTriggerEnter(Collider2D collision) { }
    protected virtual void OnEntityTriggerLeave(Collider2D collision) { }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEntityCollisionEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnEntityCollisionLeave(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEntityTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnEntityTriggerLeave(collision);
    }
}
