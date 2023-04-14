using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class AutomatedTestPotionPickup : IAutomatedTestPlayer
{
    [SerializeField] private float gotoRadius = 5f;
    [SerializeField][Range(0, 1)] private float hpTreshold = 0.5f;
    private PotionBehavior _lastPotion;

#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom)
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(drawFrom.position, Vector3.forward, gotoRadius, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (testController.Player.Health.Normalized > hpTreshold) { return false; }
        if (testController.Target == _lastPotion?.transform)
        {
            testController.Player.Controller.SetLookAt(testController.Target.position);
            return true;
        }

        List<PotionBehavior> potionBehaviours = testController.GetOverllapedComponentsInCircle<PotionBehavior>(testController.transform, gotoRadius, 10);
        if (potionBehaviours.Count == 0) { return false; }
        testController.Target = LinearAlgebraUtilities.GetClosestObject(potionBehaviours, testController.transform).transform;
        testController.Player.Controller.SetLookAt(testController.Target.position);
        return true;
    }
}
