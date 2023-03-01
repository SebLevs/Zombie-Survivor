using UnityEngine;
using System;

public class UIManager : Manager<UIManager>
{
    [field:Header("UI Audio")]
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    
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
    [field: SerializeField] public ViewTitleScreen ViewTitleScreen { get; private set; }
    [field: SerializeField] public ViewElementOptions ViewOptionMenu { get; private set; }
    [field: SerializeField] public ViewElement ViewBlackScreen { get; private set; }
    [field: SerializeField] public ViewElement ViewDeathScreen { get; private set; }
    [field: Space(10)]

    [field: SerializeField] public ViewPlayerCooldowns ViewPlayerCooldowns { get; private set; }
    [field: SerializeField] public ViewPlayerStats ViewPlayerStats { get; private set; }
    [field: Space(10)]

    [field: SerializeField] public ViewFillingBarWithCounter ViewPlayerHealthBar { get; private set; }
    [field: SerializeField] public ViewFillingBarWithCounter ViewPlayerCurrencyBar { get; private set; }
    [field: SerializeField] public ViewBossHealthBars ViewBossHealthBars { get; private set; }

    [field: SerializeField] public ViewInteract ViewInteract { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        AudioSource = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Syncronous switch view: <br/>
    /// OnHide AND OnShow at the same remainingTime
    /// <br/><br/>
    /// OnHide() current view<br/>
    /// OnShow() newly selected view
    /// </summary>
    public void OnSwitchViewSynchronous(ViewElement newView, Action hideCallback = null, Action showCallback = null)
    {
        if (CurrentView == newView)
        {
#if UNITY_EDITOR
            Debug.LogWarning("WARNING: Tried to switch view asynchronous from current view to itself"); 
#endif
            return; 
        }

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
        Entity_Player player = Entity_Player.Instance;

        ViewPlayerHealthBar.OnShow();
        ViewPlayerCurrencyBar.OnShow();
        ViewPlayerCooldowns.OnShow();
        ViewPlayerStats.OnShow( () =>
        {
            player.RefreshPlayerStats();
        });

        player.RefreshHealthBar();
        player.RefreshGoldBar();
    }

    public void HideHUD()
    {
        if (ViewPlayerHealthBar.gameObject.activeSelf && ViewPlayerHealthBar != CurrentView)
        {
            ViewPlayerHealthBar.OnHideQuick();
        }

        if (ViewPlayerCurrencyBar.gameObject.activeSelf && ViewPlayerCurrencyBar != CurrentView)
        {
            ViewPlayerCurrencyBar.OnHideQuick();
        }

        if (ViewPlayerCooldowns.gameObject.activeSelf && ViewPlayerCooldowns != CurrentView)
        {
            ViewPlayerCooldowns.OnHideQuick();
        }

        if (ViewPlayerStats.gameObject.activeSelf && ViewPlayerStats != CurrentView)
        {
            ViewPlayerStats.OnHideQuick();
        }

        if (ViewInteract.gameObject.activeSelf)
        {
            ViewPlayerStats.OnHideQuick();
        }
    }

    public void TransitionToDeathScreenView()
    {
        if (ViewDeathScreen.gameObject.activeSelf) { return; }

        ViewBossHealthBars.OnHideQuick();
        HideHUD();
        OnSwitchViewSynchronous(ViewDeathScreen, showCallback: () =>
        {
            TimerManager.Instance.AddSequentialStopwatch(_returnToTitleScreenOnDeathWaitTime, () =>
            {
                SceneLoadManager.Instance.GoToTitleScreen();
            });
        });
    }

    public void ResetCooldownView(ViewFillingBarWithCounter view)
    {
        view.Counter.Element.text = "";
        view.Filler.UnfillCompletely();
    }

    public void RefreshCooldownVisuals(ViewFillingBarWithCounter view, string remainingTime, float fillingNormalized)
    {
        view.Counter.Element.text = remainingTime;
        view.Filler.SetFilling(fillingNormalized);
    }
}
