using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint2D : MonoBehaviour
{
    [Header("Random position modifiers")]
    [Min(0)][SerializeField] private float _verticalRadius;
    [Min(0)][SerializeField] private float _horizontalRadius;
    
    public Vector2 GetRandomSpawnPoint()
    {
        Vector2 position = transform.position;
        float horizontalPoint = Random.Range(position.x - _horizontalRadius, position.x + _horizontalRadius);
        float verticalPoint = Random.Range(position.y - _verticalRadius, position.y + _verticalRadius);

        return new Vector2(horizontalPoint, verticalPoint);
    }
}
