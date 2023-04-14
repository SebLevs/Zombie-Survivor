using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

[Serializable]
public class AutomatedTestDodge : IAutomatedTestPlayer
{
    [SerializeField] private float dodgeRadius = 1.5f;
    private const int maxComponentFromOverlap = 100;

#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom)
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(drawFrom.position, Vector3.forward, dodgeRadius, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        List<ProjectileEnemy> projectiles = testController.GetOverllapedComponentsInCircle<ProjectileEnemy>(testController.transform, dodgeRadius, maxComponentFromOverlap);
        if (projectiles.Count > 0)
        {
            Transform closestProjectile = LinearAlgebraUtilities.GetClosestObject(projectiles, testController.transform).transform;
            if (closestProjectile)
            {
                testController.Player.Controller.SetLookAt(closestProjectile.transform.position);
            }
        }
        else
        {
            List<Enemy> enemies = testController.GetOverllapedComponentsInCircle<Enemy>(testController.transform, dodgeRadius, maxComponentFromOverlap);
            if (enemies.Count == 0) { return false; }
            Transform closestEnemy = LinearAlgebraUtilities.GetClosestObject(enemies, testController.transform)?.transform;
            testController.Player.Controller.SetLookAt(closestEnemy.transform.position);
        }

        var action = new PlayerAction(PlayerActionsType.DODGE);
        testController.Player.DesiredActions.AddAction(action);

        return true;
    }
}
