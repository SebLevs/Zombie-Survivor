using System;
using UnityEngine;

[Serializable]
public class AutomatedTestStartBossEncounter : IAutomatedTestPlayer
{
    [SerializeField] private float minDistanceToSpawn = 7.5f;
    [SerializeField] private float maxDistanceToSpawn = 10f;
    private float ClampedMaxDistanceToSpawn => minDistanceToSpawn > maxDistanceToSpawn ? minDistanceToSpawn : maxDistanceToSpawn;

#if UNITY_EDITOR
    public void DrawHandleGizmo(PlayerAutomatedTestController testController)
    {
    }
#endif

    public bool ExecuteTest(PlayerAutomatedTestController testController)
    {
        PortalBehavior portalBehaviour = PortalManager.Instance?.currentActivePortal;
        if (!portalBehaviour) { return false; }
        if (portalBehaviour.IsInteractable)
        {
            testController.Target = portalBehaviour.transform;
            testController.Player.Controller.SetLookAt(testController.Target.position);
            return true;
        }
        else if (testController.Target == portalBehaviour.transform)
        {
            testController.Player.Controller.SetLookAt(testController.Target.position);

            if (testController.HasReachedTarget())
            {
                testController.Target = portalBehaviour.GetBossSpawnPoint();
                testController.Player.Controller.SetLookAt(testController.Target.position);
            }

            return true;
        }
        else if (portalBehaviour.HasBossStarted)// Set target to spawn point if far enough from it
        {
            Vector2 spawnPointPosition = portalBehaviour.GetBossSpawnPoint().position;
            Vector2 playerPosition = testController.Player.transform.position;
            float distanceToBossSpawnPoint = LinearAlgebraUtilities.GetDistance2D(spawnPointPosition, playerPosition);

            if (distanceToBossSpawnPoint > ClampedMaxDistanceToSpawn) // run towards boss
            {
                testController.Target = portalBehaviour.GetBossSpawnPoint();
                testController.Player.Controller.SetLookAt(testController.Target.position);
                return true;
            }
            else if (distanceToBossSpawnPoint <= minDistanceToSpawn) // run away from boss
            {
                testController.Target = portalBehaviour.GetBossSpawnPoint();
                testController.Player.Controller.SetLookAt(testController.Target.position);

                Vector2 modifier = testController.Player.Controller.normalizedLookDirection * -1f * distanceToBossSpawnPoint;
                testController.SetBackupTargetPosition(playerPosition + modifier);
                testController.SetTargetAsBackup();
                return true;
            }
        }

        return false;
    }
}
