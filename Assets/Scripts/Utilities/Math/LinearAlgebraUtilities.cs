using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public static float GetDistance3D(Vector3 target, Vector3 from)
    {
        // Reminder pythagoeran theorem
        // C^2 = sqrt(a^2 + b^2)
        return Vector3.Distance(target, from);
    }

    /// <summary>
    /// Returns distance using pythagorean theorem
    /// </summary>
    public static float GetDistance2D(Vector2 target, Vector2 from)
    {
        // Reminder pythagoeran theorem
        // C^2 = sqrt(a^2 + b^2)
        return Vector2.Distance(target, from);
    }

    public static T GetClosestObject<T>(ICollection<T> enumerable, Transform from) where T : Component
    {
        if (enumerable.Count == 1) { return enumerable.ElementAt(0); }

        T closestComponent = enumerable.ElementAt(0);
        float leastDistance = Vector2.Distance(from.position, enumerable.ElementAt(0).transform.position);
        for (int i = 1; i < enumerable.Count; i++)
        {
            T currentEnumerable = enumerable.ElementAt(i);
            float currentDistance = Vector2.Distance(from.position, currentEnumerable.transform.position);
            if (currentDistance < leastDistance)
            {
                leastDistance = currentDistance;
                closestComponent = currentEnumerable;
            }
        }

        return closestComponent;
    }
}
