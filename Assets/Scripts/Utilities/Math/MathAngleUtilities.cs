using UnityEngine;

public class MathAngleUtilities
{
    static public float GetSignedAngle3D(Transform targetTransform, Transform myTransform)
    {
        Vector3 direction = targetTransform.position - myTransform.position;
        return Vector3.SignedAngle(direction, myTransform.forward, Vector3.up);
    }

    /// <summary>
    /// Get value in signed 180 degrees and in a counter clockwise manner<br/>
    /// transform.right = 0 degrees
    /// </summary>
    static public float GetSignedAngle2D(Transform targetTransform, Transform myTransform)
    {
        Vector2 direction = targetTransform.position - myTransform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    static public int GetAngleAsIndex3D(float angle, int lastIndex = 0)
    {
        // Front Sprites
        if (angle > -22.5f && angle < 22.6f) // South
            return 0;
        if (angle >= 22.5f && angle < 67.5f) // South-East
            return 7;
        if (angle >= 67.5f && angle < 112.5f) // East
            return 6;
        if (angle >= 112.5f && angle < 157.5f) // North-East
            return 5;

        // Back Sprites
        if (angle <= -157.5f || angle >= 157.5f) // North
            return 4;
        if (angle >= -157.4f && angle < -112.5f) // North-West
            return 3;
        if (angle >= -112.5f && angle < -67.5f) // West
            return 2;
        if (angle >= -67.5f && angle <= -22.5f) // South-West
            return 1;

        return lastIndex;
    }

    /// <summary>
    /// Get angle as an index from 0 to 7 from transform.right and in a counter clockwise manner<br/>
    /// where transform.right = 0 and transform.up = 2
    /// </summary>
    static public int GetAngleAsIndex2D(float angle, int lastIndex = 0) // TODO: To be finished
    {
        // Top angles
/*        if (angle <= -157.5f || angle >= 157.5f) // North
            return 0;
        if (angle >= -157.4f && angle < -112.5f) // North-West
            return 1;
        if (angle >= -112.5f && angle < -67.5f) // West
            return 2;
        if (angle >= -67.5f && angle <= -22.5f) // South-West
            return 3;*/

        // Left-Right angles
        if ((angle >= -45f && angle <= 0f) || (angle <= 45f && angle >= 0f)) { return 1; } // right
        if ((angle >= 135f && angle <= 180f) || (angle <= -135f && angle >= -180f)) { return 5; } // left

        // Bottom angles
/*        if (angle > 45f && angle < 135f) // South
            return 0;
        if (angle >= 22.5f && angle < 67.5f) // South-East
            return 7;
        if (angle >= 67.5f && angle < 112.5f) // East
            return 6;
        if (angle >= 112.5f && angle < 157.5f) // North-East
            return 5;*/

        return lastIndex;
    }

    /// <summary>
    /// Get angle as an index from 0 to 3 from transform.right and in a counter clockwise manner<br/>
    /// where transform.right = 0 and transform.up = 1<br/>
    /// angles are of 45 degrees
    /// </summary>
    static public int GetAngleAsIndex2D_Quad(float angle, int lastIndex = 0)
    {
        if ((angle >= 0f   && angle <= -45f) || (angle <= 45f   && angle >= 0f))    { return 0; } // right
        if ((angle >= 135f && angle <= 180f) || (angle <= -135f && angle >= -180f)) { return 2; } // left

        if (angle > 45f   && angle < 135f) { return 1; } // top
        if (angle > -135f && angle < -45f) { return 3; } // bottom

        return lastIndex;
    }

    /// <summary>
    /// Considering the angle comes from a transform.forward
    /// </summary>
    static public void FlipSprited3D(Transform spriteTransform, float angle)
    {
        Vector3 tempLocalScale = Vector3.one;
        if (angle > 0)
        {
            tempLocalScale.x *= -1.0f;
        }

        spriteTransform.localScale = tempLocalScale;
    }

    /// <summary>
    /// Considering an angle of 0 at transform.right
    /// </summary>
    static public void FlipSprite2D(Transform spriteTransform, float angle)
    {
        float absAngle = Mathf.Abs(angle);
        Vector3 tempLocalScale = Vector3.one;
        if (absAngle > 90)
        {
            tempLocalScale.x *= -1.0f;
        }

        spriteTransform.localScale = tempLocalScale;
    }
}
