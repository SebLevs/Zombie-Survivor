using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [field:SerializeField] public Health HP { get; private set; }

    public void OnGetFromAvailable()
    {
        throw new System.NotImplementedException();
    }

    public void OnReturnToAvailable()
    {
        throw new System.NotImplementedException();
    }
}
