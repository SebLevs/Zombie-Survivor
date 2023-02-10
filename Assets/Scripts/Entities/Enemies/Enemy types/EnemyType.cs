using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyType : MonoBehaviour
{
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

    public abstract void ReturnToPool(Enemy key);
}
