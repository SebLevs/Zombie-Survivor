using UnityEngine;

public class PositionGetter2DCircle : PositionGetter2D
{
    [Header("Specificity")]
    [SerializeField] private bool isInPerimeter = false;

    [Header("Random location modifiers")]
    [Min(0)][SerializeField] private float _radiusModifier;

    override public Vector2 GetRandomPosition()
    {
        Vector2 directionNormalized = (isInPerimeter) ? Random.insideUnitCircle.normalized : Random.insideUnitCircle;
        Vector2 from = transform.position;
        Vector2 directionWithModifier = from + directionNormalized * _radiusModifier;

        return directionWithModifier;
    }

    public void SetRadius(float radius)
    {
        _radiusModifier = radius < 0 ? 0 : radius;
    }
}
