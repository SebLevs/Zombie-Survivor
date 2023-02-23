using System.Collections.Generic;
using UnityEngine;

public class PortalManager : Manager<PortalManager>
{
    [SerializeField] private GameObject bossLevelBoundariesH;
    [SerializeField] private GameObject bossLevelBoundariesV;

    public List<PortalBehavior> allPortals;
    public PortalBehavior currentActivePortal;
    public Transform bossSpawnPoint;
    
    protected override void OnStart()
    {
        base.OnStart();
        currentActivePortal = allPortals[Random.Range(0, allPortals.Count)];
        currentActivePortal.ActivatePortal();
        bossSpawnPoint = currentActivePortal.GetBossSpawnPoint();
    }

    public void ActivateBossLevelBoundaries()
    {
        bossLevelBoundariesV.SetActive(true);
        bossLevelBoundariesH.SetActive(true);
    }
}
