using System;
using UnityEngine;

[Serializable]
public class AutomatedTestSecondaryAttack : IAutomatedTestPlayer
{
    [SerializeField] private float attackFromRange = 5;

    public void DrawHandleGizmo(Transform drawFrom)
    {
    }

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (!testController.TrySetNextAttackCursorPosition(attackFromRange)) { return false; }

        var action = new PlayerAction(PlayerActionsType.SPECIALSHOOT);
        Entity_Player.Instance.DesiredActions.AddAction(action);
        return true;
    }
}
