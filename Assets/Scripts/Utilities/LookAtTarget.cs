using UnityEngine;
using UnityEngine.Events;

public class LookAtTarget : MonoBehaviour, IFixedUpdateListener
{
    [Header("Choose one method from the class")]
    [SerializeField] private UnityEvent m_LookatEvent;

    [Header("Target specifications")]
    [SerializeField] private bool _isLookAtPlayer = false;
    [Tooltip("If not targeting player, a Target must be specified")]
    [SerializeField] private Transform _target;

    private void Awake()
    {
        if (_isLookAtPlayer)
        {
            _target = Entity_Player.Instance.transform;
        }
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromFixedUpdate(this);
        }
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToFixedUpdate(this);
    }

    public void OnFixedUpdate()
    {
        if (!_target) { return; }
        m_LookatEvent.Invoke();
    }

    private Vector2 GetDirectionToTarget()
    {
        return _target.position - transform.position;
    }

    public void LookAtTargetFromUp()
    {
        this.transform.up = GetDirectionToTarget();
    }

    public void LookAtTargetFromBottom()
    {
        this.transform.up = GetDirectionToTarget() * -1;
    }

    public void LookAtTargetFromRight()
    {
        this.transform.right = GetDirectionToTarget();
    }

    public void LookAtTargetFromLeft()
    {
        this.transform.right = GetDirectionToTarget() * -1;
    }

    public void LookAtTargetFromForward()
    {
        this.transform.forward = GetDirectionToTarget();
    }

    public void LookAtTargetFromBackward()
    {
        this.transform.forward = GetDirectionToTarget() * -1;
    }
}
