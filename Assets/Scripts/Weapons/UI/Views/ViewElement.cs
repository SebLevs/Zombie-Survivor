using UnityEngine;
using System;
using UnityEngine.Events;

public class ViewElement : MonoBehaviour
{
    private int _onShowHash;
    public Action m_onShowAction;
    [SerializeField] private UnityEvent m_defaultShowEvent;

    private int _onHideHash;
    private Action m_onHideAction;
    [SerializeField] private UnityEvent m_defaultHideEvent;

    protected Animator m_animator;

    private void Awake() { OnAwake(); }

    protected virtual void OnAwake()
    {
        m_animator = GetComponent<Animator>();
        _onShowHash = Animator.StringToHash("Show");
        _onHideHash = Animator.StringToHash("Hide");
    }

    private void Start() { OnStart(); }

    protected virtual void OnStart() { }

    public void OnShow() { StopAllCoroutines(); OnShow(null); }
    public void OnShowQuick() { StopAllCoroutines(); OnShowQuick(null); }
    public void OnHide() { StopAllCoroutines(); OnHide(null); } 
    public void OnHideQuick() { StopAllCoroutines(); OnHideQuick(null); }

    public virtual void OnShow(Action callback = null)
    {
        if (gameObject.activeSelf) { return; }

        gameObject.SetActive(true);
        m_onShowAction = callback;
        m_animator.SetTrigger(_onShowHash);
    }

    public virtual void OnShowQuick(Action callback = null)
    {
        if (gameObject.activeSelf) { return; }

        gameObject.SetActive(true);
        callback += () => m_animator.speed = 1f;
        m_onShowAction = callback;

        m_animator.SetTrigger(_onShowHash);
        m_animator.speed = 10f;
    }

    public virtual void OnHide(Action callback = null)
    {
        if (!gameObject.activeSelf) { return; }

        callback += () => gameObject.SetActive(false);
        m_onHideAction = callback;
        m_animator.SetTrigger(_onHideHash);
    }

    public virtual void OnHideQuick(Action callback = null)
    {
        if (!gameObject.activeSelf) { return; }

        callback += () => gameObject.SetActive(false);
        callback += () => m_animator.speed = 1f;
        m_onHideAction = callback;

        m_animator.SetTrigger(_onHideHash);
        m_animator.speed = 10f;
    }

    public void AnimationEvent_HideCallback()
    {
        TryCallback(m_onHideAction);
        m_defaultHideEvent?.Invoke();
    }

    public void AnimationEvent_ShowCallback()
    {
        TryCallback(m_onShowAction);
        m_defaultShowEvent?.Invoke();
    }

    protected void TryCallback(Action _callback = null)
    {
        if (_callback != null)
        {
            _callback.Invoke();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}