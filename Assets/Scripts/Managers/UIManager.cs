using UnityEngine;
using System;

public class UIManager : Manager<UIManager>
{
    [field: Header("Character print")]
    [field:SerializeField] public float CharacterPrintSpeed { get; private set; }
    [field:SerializeField] public float LinePrintPause { get; private set; }

    // TEST
    [field: Header("Views")]
    [field: SerializeField] public ViewElement CurrentView { get; private set; }

    /// <summary>
    /// Syncronous switch view: <br/>
    /// OnHide AND OnShow at the same time
    /// <br/><br/>
    /// OnHide() current view<br/>
    /// OnShow() newly selected view
    /// </summary>
    public void OnSwitchViewSynchronous(ViewElement newView, Action hideCallback = null, Action showCallback = null)
    {
        // Hide currently selected view
        if (CurrentView)
        {
            //CurrentView.StopAllCoroutines();
            CurrentView.OnHide(callback: hideCallback);
        }

        CurrentView = newView;

        // m_currentStateView is set at end of onShow
        //newView.StopAllCoroutines();
        newView.OnShow(callback: showCallback);
    }

    /// <summary>
    /// Squential switch view: <br/>
    /// OnHide THEN OnShow
    /// <br/><br/>
    /// OnHide() current view<br/>
    /// OnShow() newly selected view
    /// </summary>
    public void OnSwitchViewSequential(ViewElement newView, Action hideCallback = null, Action showCallback = null)
    {
        // Hide currently selected view
        if (CurrentView && CurrentView != newView)
        {
            CurrentView.OnHide(callback: () =>
            {
                if (hideCallback != null) { hideCallback.Invoke(); }
                CurrentView = newView;
                newView.OnShow(callback: showCallback);
            });
        }
        else
        {
            CurrentView = newView;
            newView.OnShow(callback: showCallback);
        }
    }

    public void ShowHUD()
    {
        // ViewName.OnShow();
    }

    public void HideHUD()
    {
        // Example
        /*if (ViewCrosshair.gameObject.activeSelf && ViewCrosshair != CurrentView)
        {
            ViewCrosshair.OnHideQuick();
        }*/
    }
}