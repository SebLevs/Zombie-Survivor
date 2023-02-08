using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{

    protected abstract void OnStart();
    protected abstract void OnAwake();
    protected abstract void OnUpdate();
    protected virtual void OnFixedUpdate() { }

    protected virtual void OnProjectileCollisionEnter(Collision2D collision) { }
    protected virtual void OnProjectileCollisionLeave(Collision2D collision) { }
    protected virtual void OnProjectileTriggerEnter(Collider2D collision) { }
    protected virtual void OnProjectileTriggerLeave(Collider2D collision) { }
    

    private void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnProjectileCollisionEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnProjectileCollisionLeave(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnProjectileTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnProjectileTriggerLeave(collision);
    }

}
