using UnityEngine;

public class PortalArrowBehavior : MonoBehaviour, IPauseListener, IFrameUpdateListener
{
    private PortalManager _portalManager;
    private PortalBehavior _portal;
    private bool _canLook;

    public void OnEnable()
    {
        _portalManager = PortalManager.Instance;
        _portal = _portalManager.currentActivePortal;
        _canLook = true;

        GameManager.Instance.SubscribeToPauseGame(this);
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public void OnDisable()
    {
        _canLook = false;
        if (GameManager.Instance)
        {
            GameManager.Instance.UnSubscribeFromPauseGame(this);
        }
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }

    }

    public void OnPauseGame()
    {
        _canLook = false;
    }

    public void OnResumeGame()
    {

        _canLook = true;
    }

    public void OnUpdate()
    {
        if (_canLook)
        {
            transform.up = _portal.transform.position - transform.position;
        }
    }
}
