using UnityEngine;

public class PositionGetter2DQuad : PositionGetter2D
{
    [Header("Random position modifiers")]
    [Min(0)][SerializeField] private float _verticalRadius;
    [Min(0)][SerializeField] private float _horizontalRadius;

    override public Vector2 GetRandomPosition()
    {
        Vector2 position = transform.position;
        float horizontalPoint = Random.Range(position.x - _horizontalRadius, position.x + _horizontalRadius);
        float verticalPoint = Random.Range(position.y - _verticalRadius, position.y + _verticalRadius);

        return new Vector2(horizontalPoint, verticalPoint);
    }
}
