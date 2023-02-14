using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 
// Try to set state controller here

public abstract class EnemyType : MonoBehaviour
{
    public StateController<Enemy> StateController { get; private set; }
    //public StateContainer // DOESNT WORK, CANT ACCESS SPECIFIC STATE
    // Issue: Specific states are innacessible due to hierarchy
    // Possible fix: Make the state controller a monobehaviour which would getcomponent the enemy class for context

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
