using System;
using UnityEngine;

[Serializable]
public class AutomatedTestPrimaryAttack : IAutomatedTestPlayer
{
    [SerializeField] private float attackFromRange = 5;

    public void DrawHandleGizmo(Transform drawFrom)
    {
    }

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (!testController.TrySetNextAttackCursorPosition(attackFromRange)) { return false; }

        var action = new PlayerAction(PlayerActionsType.SHOOT);
        Entity_Player.Instance.DesiredActions.AddAction(action);
        return true;
    }
}
