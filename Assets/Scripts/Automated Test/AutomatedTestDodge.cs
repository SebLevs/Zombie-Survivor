using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[Serializable]
public class AutomatedTestDodge : IAutomatedTestPlayer
{
    [SerializeField] private float dodgeRadius = 1.5f;
    private const int maxComponentFromOverlap = 100;

#if UNITY_EDITOR
    public void DrawHandleGizmo(PlayerAutomatedTestController testController)
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(testController.transform.position, Vector3.forward, dodgeRadius, 2f);

        Vector2 normalizedLookAt = Entity_Player.Instance.Controller.normalizedLookDirection;
        Vector2 point = (Vector2)testController.transform.position + normalizedLookAt * (dodgeRadius * 0.5f);
        Handles.DrawWireDisc(point, Vector3.forward, dodgeRadius * 0.5f, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (!TryDodgeWhenFacingObstacles(testController))
        {
            if (!TryDodgeNearProjectiles(testController))
            {
                if (!TryDodgeNearEnemies(testController))
                {
                    return false;
                }
            }
        }

        var action = new PlayerAction(PlayerActionsType.DODGE);
        testController.Player.DesiredActions.AddAction(action);
        return true;
    }

    private bool TryDodgeWhenFacingObstacles(PlayerAutomatedTestController testController)
    {
        Vector2 normalizedLookAt = Entity_Player.Instance.Controller.normalizedLookDirection;
        Vector2 point = (Vector2)testController.Player.transform.position + normalizedLookAt * (dodgeRadius * 0.5f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(point, dodgeRadius * 0.5f);

        List<Transform> validHits = new();
        foreach (var hit in hits)
        {
            if (hit == null) { break; }

            if (hit.gameObject.layer == 7) // Obstacle
            {
                validHits.Add(hit.transform);
            }
            else if (hit.gameObject.layer == 8) // Enemy
            {
                validHits.Add(hit.transform);
            }
        }

        if (validHits.Count == 0) { return false; }
        return true;
    }

    private bool TryDodgeNearProjectiles(PlayerAutomatedTestController testController)
    {
        List<ProjectileEnemy> projectiles = testController.GetOverllapedComponentsInCircle<ProjectileEnemy>(testController.transform, dodgeRadius, maxComponentFromOverlap);

        if (projectiles.Count == 0) { return false; }

        Transform closestProjectile = LinearAlgebraUtilities.GetClosestObject(projectiles, testController.transform).transform;
        testController.Player.Controller.SetLookAt(closestProjectile.transform.position);
        return true;
    }

    private bool TryDodgeNearEnemies(PlayerAutomatedTestController testController)
    {
        List<Enemy> enemies = testController.GetOverllapedComponentsInCircle<Enemy>(testController.transform, dodgeRadius, maxComponentFromOverlap);

        if (enemies.Count == 0) { return false; }

        Transform closestEnemy = LinearAlgebraUtilities.GetClosestObject(enemies, testController.transform)?.transform;
        testController.Player.Controller.SetLookAt(closestEnemy.transform.position);
        return true;
    }
}
