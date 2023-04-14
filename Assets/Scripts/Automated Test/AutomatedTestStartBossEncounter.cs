using System;
using UnityEngine;

[Serializable]
public class AutomatedTestStartBossEncounter : IAutomatedTestPlayer
{
#if UNITY_EDITOR
    public void DrawHandleGizmo(Transform drawFrom)
    {
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        // TODO: check if enough gold for boss portal. If not, return false
        // TODO: set target as portal
        PortalBehavior portalBehaviour = PortalManager.Instance.currentActivePortal;

        if (testController.Target == portalBehaviour?.transform)
        {
            testController.Player.Controller.SetLookAt(testController.Target.position);

            if (testController.HasReachedTarget())
            {
                Debug.Log("REACHED PORTAL, SET BOSS AS TARGET");
                // TODO: Check if portal manager has a "boss encounter started" boolean, else set one here
            }

            return true;
        }

        return false;
    }
}
