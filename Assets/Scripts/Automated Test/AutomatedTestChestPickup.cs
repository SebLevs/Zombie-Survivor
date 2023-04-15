using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class AutomatedTestChestPickup : IAutomatedTestPlayer
{
    [SerializeField] private float gotoRadius = 5f;
    private ChestBehavior _lastChest;

#if UNITY_EDITOR
    public void DrawHandleGizmo(PlayerAutomatedTestController testController)
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(testController.transform.position, Vector3.forward, gotoRadius, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (_lastChest != null && testController.Target == _lastChest.transform)
        {
            testController.Player.Controller.SetLookAt(testController.Target.position);

            if (testController.HasReachedTarget())
            {
                _lastChest.TryOpenChest();
                testController.SetBackupTargetPosition(testController.Player.transform);
                testController.SetTargetAsBackup();
            }

            return true;
        }

        List<ChestBehavior> chestBehaviour = testController.GetOverllapedComponentsInCircle<ChestBehavior>(testController.transform, gotoRadius, 10);

        List<ChestBehavior> interactableChests = new();
        foreach (ChestBehavior chestBehavior in chestBehaviour)
        {
            if (chestBehavior == null || !chestBehavior.CanOpenChest) { break; }
            if (chestBehavior.isInteractable) { interactableChests.Add(chestBehavior); }
        }

        if (interactableChests.Count == 0) { return false; }
        _lastChest = LinearAlgebraUtilities.GetClosestObject(interactableChests, testController.transform);
        testController.Target = _lastChest.transform;
        testController.Player.Controller.SetLookAt(testController.Target.position);
        return true;
    }
}
