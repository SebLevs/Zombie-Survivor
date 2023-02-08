using UnityEngine;
using System;

public class ViewElement : MonoBehaviour
{
    private int _onShowHash;
    public Action m_onShowAction;

    private int _onHideHash;
    private Action m_onHideAction;

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

    public virtual void OnShow(Action callback = null)
    {
        if (gameObject.activeSelf) { return; }

        gameObject.SetActive(true);
        m_onShowAction = callback;
        m_animator.SetTrigger(_onShowHash);
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
        callback += () => gameObject.SetActive(false);
        callback += () => m_animator.speed = 1f;
        m_onHideAction = callback;

        m_animator.SetTrigger(_onHideHash);
        m_animator.speed = 10f;
    }

    public void AnimationEvent_HideCallback()
    {
        TryCallback(m_onHideAction);
    }

    public void AnimationEvent_ShowCallback()
    {
        TryCallback(m_onShowAction);
    }

    protected void TryCallback(Action _callback = null)
    {
        if (_callback != null)
        {
            _callback.Invoke();
        }
    }
}