using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    private Health m_hp;
    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnGetFromAvailable()
    {
        throw new System.NotImplementedException();
    }

    public void OnReturnToAvailable()
    {
        m_rigidbody.velocity = Vector2.zero;
    }
}
