using System;
using UnityEngine;
using static UnityEngine.UI.Image;
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

        Vector2 origin = testController.transform.position;
        Vector2 direction = ((Vector2)testController.Target.position - origin).normalized;
        Vector2 endPoint = origin + direction * moveAtRange;
        Handles.DrawLine(origin, endPoint, 2f);
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
        //Vector2 validPoint = LinearAlgebraUtilities.GetPointAlongPerimeter2D(testController.Target.position, moveAtRange);
        //testController.SetBackupTargetPosition(validPoint);
        //testController.SetTargetAsBackup();

        Vector2 validPoint = testController.transform.position;

        for (int i = 0; i < 5; i++)
        {
            Vector2 newPoint = LinearAlgebraUtilities.GetPointAlongPerimeter2D(testController.Target.position, moveAtRange);

            Vector2 origin = testController.transform.position;
            Vector2 direction = (newPoint - origin).normalized;
            RaycastHit2D[] hits = new RaycastHit2D[1];
            
            Physics2D.RaycastNonAlloc(origin, direction, hits, moveAtRange, 7); // Obstacle
            if (!hits[0].collider)
            {
                Physics2D.RaycastNonAlloc(origin, direction, hits, moveAtRange, 8); // Enemy
                if (!hits[0].collider)
                {
                    Debug.Log("A");
                    validPoint = newPoint;
                    break;
                }

            }
            Debug.Log("obstacle or enemy in line");
        }

        testController.SetBackupTargetPosition(validPoint);
        testController.SetTargetAsBackup();
    }
}
