using System;
using UnityEngine;

[Serializable]
public class AutomatedTestStartBossEncounter : IAutomatedTestPlayer
{
    public void DrawHandleGizmo(Transform drawFrom)
    {
    }

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        // TODO: check if enough gold for boss portal. If not, return false
        // TODO: set target as portal
        return true;
    }
}
