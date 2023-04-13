using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class AutomatedTestChestPickup : IAutomatedTestPlayer
{
    [SerializeField] private float pickupRadius = 5f;
    [SerializeField] private float openChestRadius = 5f;
    [SerializeField][Range(0, 1)] private float goldThreshold = 0.5f;
    private ChestBehavior _lastChest;

#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom)
    {
        Handles.DrawWireDisc(drawFrom.position, Vector3.forward, pickupRadius, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        


        if (testController.Target == _lastChest?.transform)
        {
            testController.Player.Controller.SetLookAt(testController.Target.position);

            // TODO: Check if end reached distance for target && ?if interactable view is up?: Interact if true

            return true;
        }

        List<ChestBehavior> chestBehaviour = testController.GetOverllapedComponentsInCircle<ChestBehavior>(testController.transform, pickupRadius, 10);

        List<ChestBehavior> interactableChests = new();
        foreach (ChestBehavior chestBehavior in chestBehaviour)
        {
            if (chestBehavior == null) { break; }
            if (chestBehavior.isInteractable) { interactableChests.Add(chestBehavior); }
        }

        if (interactableChests.Count == 0) { return false; }
        testController.Target = LinearAlgebraUtilities.GetClosestObject(interactableChests, testController.transform).transform;
        testController.Player.Controller.SetLookAt(testController.Target.position);
        return true;
    }
}
