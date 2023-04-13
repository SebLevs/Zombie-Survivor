using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class AutomatedTestChestPickup : IAutomatedTestPlayer
{
    [SerializeField] private float pickupRadius = 5f;

#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom)
    {
        Handles.DrawWireDisc(drawFrom.position, Vector3.forward, pickupRadius, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        return true;
    }
}
