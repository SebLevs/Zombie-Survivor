using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewElementButton : ViewElement
{
    [SerializeField] private bool _trackLastSelected = false;
    [SerializeField] protected Selectable m_defaultSelectable;
    protected Selectable m_lastSelectable;
    protected CanvasGroup m_canvasGroup;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        SelectDefaultSelectableElement();
    }

    protected void SelectDefaultSelectableElement()
    {
        if (m_defaultSelectable)
        {
            m_defaultSelectable.Select();
        }
    }

    public override void OnShow(Action callback = null)
    {
        callback += OnShowCallback;
        base.OnShow(callback);
    }

    public override void OnHide(Action callback = null)
    {
        if (!gameObject.activeSelf) { return; }

        SetInteractability(false);

        callback += OnHideCallback;
        base.OnHide(callback);

        if (_trackLastSelected)
        {
            m_lastSelectable = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        }

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public override void OnHideQuick(Action callback = null)
    {
        callback += OnHideCallback;
        base.OnHideQuick(callback);
    }

    protected virtual void OnShowCallback()
    {
        if (!m_lastSelectable) { m_lastSelectable = m_defaultSelectable; }
        if (m_lastSelectable) { m_lastSelectable.Select(); }
        SetInteractability(true);
    }

    protected virtual void OnHideCallback() { }

    protected void SetInteractability(bool interactability)
    {
        m_canvasGroup.interactable = interactability;
        m_canvasGroup.blocksRaycasts = interactability;
    }
}
