using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float hitValue;

    public SpriteRenderer SpriteRenderer { get; private set; }

    private void Start()
    {
        // ALT + Enter = allows to creates new variable
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        hitValue = Mathf.Lerp(hitValue, 0, Time.deltaTime * 5);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitValue = 1;
        }

        // ALT + Arrow UP or Arrow Down == move line around
        SpriteRenderer.material.SetFloat("_HitValue", hitValue); // change material of shader
    }
}
