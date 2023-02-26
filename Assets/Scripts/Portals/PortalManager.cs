using System.Collections.Generic;
using UnityEngine;

public class PortalManager : Manager<PortalManager>
{
    [SerializeField] private GameObject bossLevelBoundariesH;
    [SerializeField] private GameObject bossLevelBoundariesV;
    [SerializeField] private GameObject bossBoundariesVisual;

    public List<PortalBehavior> allPortals;
    public PortalBehavior currentActivePortal;
    public Transform bossSpawnPoint;
    
    protected override void OnStart()
    {
        base.OnStart();
        currentActivePortal = allPortals[Random.Range(0, allPortals.Count)];
        currentActivePortal.ActivatePortal();
        bossSpawnPoint = currentActivePortal.GetBossSpawnPoint();
        //Entity_Player.Instance.arrow.enabled = true;
        Entity_Player.Instance.arrow.SetTargetAsPortal();
    }

    public void ActivateBossLevelBoundaries()
    {
        bossLevelBoundariesV.SetActive(true);
        bossLevelBoundariesH.SetActive(true);
        bossBoundariesVisual.SetActive(true);
    }
}
