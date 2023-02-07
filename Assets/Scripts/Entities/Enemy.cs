using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private ImageFiller m_healthBar;
    private Health m_hp;

    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
    }

    public void OnGetFromAvailable()
    {
        m_healthBar.SetFilling(m_hp.Normalized);
    }

    public void OnReturnToAvailable()
    {
        m_rigidbody.velocity = Vector2.zero;
    }
}
