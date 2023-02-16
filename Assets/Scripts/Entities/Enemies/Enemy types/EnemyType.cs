using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 
// Try to set state controller here

public abstract class EnemyType : MonoBehaviour
{
    protected Enemy m_context;

    private void Awake()
    {
        OnAwake();
    }
    protected virtual void OnAwake() { }

    private void Start()
    {
        OnStart();
    }
    protected virtual void OnStart() { }

    public abstract void ReturnToPool();

    public void Init(Enemy context)
    {
        m_context = context;
    }
}
