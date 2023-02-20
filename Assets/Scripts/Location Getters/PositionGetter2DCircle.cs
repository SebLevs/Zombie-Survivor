using UnityEngine;

public class PositionGetter2DCircle : PositionGetter2D
{
    [Header("Random location modifiers")]
    [Min(0)][SerializeField] private float _radiusModifier;

    override public Vector2 GetRandomPosition()
    {
        Vector2 directionNormalized = Random.insideUnitCircle; // Unit vector in a circle
        Vector2 from = transform.position;
        Vector2 directionWithModifier = from + directionNormalized * _radiusModifier;

        return directionWithModifier;
    }
}
