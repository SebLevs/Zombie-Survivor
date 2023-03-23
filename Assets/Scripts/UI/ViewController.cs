using System;
using Unity.VisualScripting;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private bool isTransitionInstantaneous = false;

    [field:Space]
    [field: SerializeField] public ViewElement DefaultView { get; private set; }
    public ViewElement CurrentView { get; private set; }

    private void Start()
    {
        if (DefaultView)
        {
            SwitchViewSequential(DefaultView);
        }
    }

    public void SwitchViewSequential(ViewElement newElement) => SwitchViewSequential(newElement, null, null);
    /// <summary>
    /// Squential switch view: <br/>
    /// OnHide THEN OnShow
    /// <br/><br/>
    /// OnHide() current view<br/>
    /// OnShow() newly selected view
    /// </summary>
    public void SwitchViewSequential(ViewElement newElement, Action hideCallback = null, Action showCallback = null)
    {
        // Hide currently selected view
        if (CurrentView && CurrentView != newElement)
        {
            Action callback = () =>
            {
                if (hideCallback != null) { hideCallback.Invoke(); }
                CurrentView = newElement;
                newElement.OnShow(callback: showCallback);
            };

            if (isTransitionInstantaneous)
            {
                CurrentView.OnHideInstantaneous(callback: callback);
            }
            else
            {
                CurrentView.OnHide(callback: callback);
            }
        }
        else
        {
            CurrentView = newElement;
            if (isTransitionInstantaneous)
            {
                newElement.OnShowInstantaneous(callback: showCallback);
            }
            else
            {
                newElement.OnShow(callback: showCallback);
            }
        }
    }


    public void SwitchViewSynchronous(ViewElement newElement) => SwitchViewSynchronous(newElement, null, null);
    /// <summary>
    /// Syncronous switch view: <br/>
    /// OnHide AND OnShow at the same remainingTime
    /// <br/><br/>
    /// OnHide() current view<br/>
    /// OnShow() newly selected view
    /// </summary>
    public void SwitchViewSynchronous(ViewElement newElement, Action hideCallback = null, Action showCallback = null)
    {
        if (CurrentView == newElement)
        {
#if UNITY_EDITOR
            Debug.LogWarning("WARNING: Tried to switch view asynchronous from current view to itself");
#endif
            return;
        }

        // Hide currently selected view
        if (CurrentView)
        {
            if (isTransitionInstantaneous)
            {
                CurrentView.OnHideInstantaneous(callback: hideCallback);
            }
            else
            {
                CurrentView.OnHide(callback: hideCallback);
            }
        }

        CurrentView = newElement;

        // m_currentStateView is set at end of onShow
        //newElement.StopAllCoroutines();
        if (isTransitionInstantaneous)
        {
            newElement.OnShowInstantaneous(callback: showCallback);
        }
        else
        {
            newElement.OnShow(callback: showCallback);
        }
    }
}
