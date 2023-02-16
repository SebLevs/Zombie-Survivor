using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    protected Rigidbody2D m_rigidBody;
    protected Collider2D m_collider;

    protected virtual void OnAwake() 
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
    }

    protected virtual void OnStart() { }

    protected virtual void OnEntityCollisionEnter(Collision2D collision) { }
    protected virtual void OnEntityCollisionLeave(Collision2D collision) { }
    protected virtual void OnEntityTriggerEnter(Collider2D collision) { }
    protected virtual void OnEntityTriggerStay(Collider2D collision) { }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnEntityTriggerStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnEntityTriggerLeave(collision);
    }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }
}
