using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [field: Header("Character print")]
    [field:SerializeField] public float CharacterPrintSpeed { get; private set; }
    [field:SerializeField] public float LinePrintPause { get; private set; }

    [Header("Death timer")]
    [SerializeField] private float _returnToTitleScreenOnDeathWaitTime = 5f;

    [field: Header("Views")]
    [field: SerializeField] public ViewElement CurrentView { get; private set; }
    [field: SerializeField] public ViewElement ViewBackgroundBlackScreen { get; private set; }
    [field: SerializeField] public ViewElement ViewEmpty { get; private set; }
    [field:Space(10)]
    [field: SerializeField] public ViewElement ViewTitleScreen { get; private set; }
    [field: SerializeField] public ViewElementOptions ViewOptionMenu { get; private set; }
    [field: SerializeField] public ViewElement ViewBlackScreen { get; private set; }
    [field: SerializeField] public ViewElement ViewDeathScreen { get; private set; }
    [field: Space(10)]

    [field: SerializeField] public ViewPlayerCooldowns ViewPlayerCooldowns;
    [field: SerializeField] public ViewPlayerStats ViewPlayerStats;
    [field: Space(10)]

    [field: SerializeField] public ViewFillingBarWithCounter ViewPlayerHealthBar;
    [field: SerializeField] public ViewFillingBarWithCounter ViewPlayerExperienceBar;

    [field:Space(10)]
    [field: Header("PlayerStats")]
/*    [field: SerializeField] public TMP_Text attackCooldown;
    [field: SerializeField] public TMP_Text boomCooldown;
    [field: SerializeField] public TMP_Text dodgeCooldown;
    [field: SerializeField] public Image attackFill;
    [field: SerializeField] public Image boomFill;
    [field: SerializeField] public Image dodgeFill;*/
/*    [field: SerializeField] public TMP_Text PS_moveSpeed;
    [field: SerializeField] public TMP_Text PS_attackCooldown;
    [field: SerializeField] public TMP_Text PS_boomCooldown;
    [field: SerializeField] public TMP_Text PS_boomDistance;
    [field: SerializeField] public TMP_Text PS_isInvincible;*/

    /// <summary>
    /// Syncronous switch view: <br/>
    /// OnHide AND OnShow at the same remainingTime
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
        ViewPlayerHealthBar.OnShow();
        ViewPlayerExperienceBar.OnShow();
        ViewPlayerCooldowns.OnShow();
        ViewPlayerStats.OnShow( () =>
        {
            Entity_Player.Instance.RefreshPlayerStats();
        });
    }

    public void HideHUD()
    {
        if (ViewPlayerHealthBar.gameObject.activeSelf && ViewPlayerHealthBar != CurrentView)
        {
            ViewPlayerHealthBar.OnHideQuick();
        }

        if (ViewPlayerExperienceBar.gameObject.activeSelf && ViewPlayerExperienceBar != CurrentView)
        {
            ViewPlayerExperienceBar.OnHideQuick();
        }

        if (ViewPlayerCooldowns.gameObject.activeSelf && ViewPlayerCooldowns != CurrentView)
        {
            ViewPlayerCooldowns.OnHideQuick();
        }

        if (ViewPlayerStats.gameObject.activeSelf && ViewPlayerStats != CurrentView)
        {
            ViewPlayerStats.OnHideQuick();
        }
    }

    public void DeathTransition()
    {
        HideHUD();
        OnSwitchViewSynchronous(ViewDeathScreen, showCallback: () =>
        {
            TimerManager.Instance.AddSequentialStopwatch(_returnToTitleScreenOnDeathWaitTime, () =>
            {
                SceneLoadManager.Instance.GoToTitleScreen();
                Entity_Player.Instance.Health.FullHeal();
            });
        });
    }

    public void ResetCooldownView(ViewFillingBarWithCounter view)
    {
        view.Counter.Element.text = "";
        view.Filler.ResetFilling();
    }

    public void  RefreshCooldownVisuals(ViewFillingBarWithCounter view, string remainingTime, float fillingNormalized)
    {
        view.Counter.Element.text = remainingTime;
        view.Filler.SetFilling(fillingNormalized);
    }
}
