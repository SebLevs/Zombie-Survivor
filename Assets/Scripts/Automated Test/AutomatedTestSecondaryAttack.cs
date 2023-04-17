using System;
using UnityEngine;

[Serializable]
public class AutomatedTestSecondaryAttack : IAutomatedTestPlayer
{
#if UNITY_EDITOR
    public void DrawHandleGizmo(PlayerAutomatedTestController testController)
    {
        Gizmos.color = Color.magenta;
        Vector2 endPoint = (Vector2)testController.transform.position + testController.Player.Controller.normalizedLookDirection * testController.Player.boomDistance;
        Gizmos.DrawLine(testController.transform.position, endPoint);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (!testController.TrySetNextAttackCursorPosition(testController.Player.boomDistance)) { return false; }

        var action = new PlayerAction(PlayerActionsType.SPECIALSHOOT);
        Entity_Player.Instance.DesiredActions.AddAction(action);
        return true;
    }
}
