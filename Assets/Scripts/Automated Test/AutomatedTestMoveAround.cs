using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class AutomatedTestMoveAround : IAutomatedTestPlayer
{
    [SerializeField] private float moveEachTime = 1f;
    [SerializeField] private float moveAtRange = 5f;
    private SequentialTimer _movementTimer;

    public void Init(PlayerAutomatedTestController testController)
    {
        _movementTimer = new SequentialTimer(moveEachTime, () =>
        {
            MoveBackupTargetPosition(testController);
            _movementTimer.Reset();
        });
        _movementTimer.StartTimer();
    }

#if UNITY_EDITOR
    public void DrawHandleGizmo(PlayerAutomatedTestController testController)
    {
        Handles.color = Color.black;
        Handles.DrawWireDisc(testController.transform.position, Vector3.forward, moveAtRange, 2f);
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        if (testController.IsTargetBackup())
        {
            _movementTimer.OnUpdateTime();
        }
        else { _movementTimer.Reset(); }

        if (testController.HasReachedTarget())
        {
            return false;
        }

        testController.Player.Controller.UpdateMoveDirection(testController.GetDirectionToTarget());

        return true;
    }

    private void MoveBackupTargetPosition(PlayerAutomatedTestController testController)
    {
        Vector2 newPoint = LinearAlgebraUtilities.GetPointAlongPerimeter2D(testController.Target.position, moveAtRange);
        testController.SetBackupTargetPosition(newPoint);
        testController.SetTargetAsBackup();
    }
}
