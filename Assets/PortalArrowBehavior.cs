using UnityEngine;

public class PortalArrowBehavior : MonoBehaviour
{
    private PortalManager _portalManager;
    private PortalBehavior _portal;
    private bool _canLook;

    private void OnEnable()
    {
        _portalManager = PortalManager.Instance;
        _portal = _portalManager.currentActivePortal;
        _canLook = true;
    }

    private void OnDisable()
    {
        _canLook = false;
    }

    void Update()
    {
        if (_canLook)
        {
            transform.up = _portal.transform.position - transform.position;
        }
    }
}
