using UnityEngine;

public static class LinearAlgebraUtilities
{
    public static Vector3 GetCursorToWorld()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Prevents a -10 on the Z axis which would rotate the sprite in an odd way
    /// </summary>
    public static Vector3 GetCursorToWorlNoZ()
    {
        Vector3 WorldSpaceCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = WorldSpaceCursorPosition.x;
        float y = WorldSpaceCursorPosition.y;
        Vector3 target = new Vector3(x, y, 0);
        return target;
    }

    /// <summary>
    /// If returned to the "forward" of the self, will rotate the self towards the target in 2 dimensions<br/>
    /// Note that visual oddities can happen if the Z value of the target is no the same as self
    /// </summary>
    public static Vector3 GetDirection2D(Vector3 target, Vector3 self)
    {
        return target - self;
    }

    public static Vector3 GetPointAlongPerimeter2D(Vector3 target, Vector3 from, float radiusModifier)
    {
        // Normalization process reminder
        // WHERE
        //  C^ = unit vector     = direction
        //  C  = (target - from)
        // |C| = magnitude of C  = sqrt(C.x^2 + C.y^2 + C.z^2)
        // FORMULA
        //  C^ = C / sqrt(|C|)
        Vector3 directionNormalized = (target - from).normalized;
        Vector3 directionFromCamToPlayer_WithRadius = from + directionNormalized * radiusModifier;

        return directionFromCamToPlayer_WithRadius;
    }

    public static Vector3 GetPointAlongPerimeter2D(Vector3 from, float radiusModifier)
    {
        // Normalization process reminder
        // WHERE
        //  C^ = unit vector     = direction
        //  C  = (target - from)
        // |C| = magnitude of C  = sqrt(C.x^2 + C.y^2 + C.z^2)
        // FORMULA
        //  C^ = C / sqrt(|C|)
        Vector3 directionNormalized = UnityEngine.Random.insideUnitCircle; // Unit vector in a circle
        Vector3 directionFromCamToPlayer_WithRadius = from + directionNormalized * radiusModifier;

        return directionFromCamToPlayer_WithRadius;
    }

    /// <summary>
    /// Returns distance using pythagorean theorem
    /// </summary>
    public static float GetDistance(Vector3 target, Vector3 from)
    {
        // Reminder pythagoeran theorem
        // C^2 = sqrt(a^2 + b^2)
        return Vector3.Distance(target, from);
    }
}
