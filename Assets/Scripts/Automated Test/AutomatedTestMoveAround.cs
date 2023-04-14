using System;
using UnityEngine;

[Serializable]
public class AutomatedTestMoveAround : IAutomatedTestPlayer
{
#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom)
    {
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        // Set timer update here to move only once every N seconds?
        // TODO: set default target position to current target if any
        // TODO: set target as default target
        // TODO: try N time to find an empty nearby spot from target
        // TODO: if any empty space is found, set target as empty spot position and move towards it
        return true;
    }
}
