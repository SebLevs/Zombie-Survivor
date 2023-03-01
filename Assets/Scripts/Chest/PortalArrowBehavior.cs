using UnityEngine;

public class PortalArrowBehavior : MonoBehaviour, IPauseListener, IUpdateListener
{
    private PortalManager _portalManager;
    private PortalBehavior _portal;
    private Transform _target;
    private bool _canLook;

    public void OnEnable()
    {
        GameManager.Instance.SubscribeToPauseGame(this);
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public void SetTargetAsPortal()
    {
        _portalManager = PortalManager.Instance;
        _target = _portalManager.currentActivePortal.transform;
        _canLook = true;
    }

    public void SetTargetAs(Transform target) => _target = target;

    public void LookAtTarget()
    {
        transform.up = _target.position - transform.position;
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
            LookAtTarget();
        }
    }
}
