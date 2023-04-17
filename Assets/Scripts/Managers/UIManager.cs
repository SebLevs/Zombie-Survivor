using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public AudioSource AudioSource { get; private set; }

    [field: Header("Character print")]
    [field:SerializeField] public float CharacterPrintSpeed { get; private set; }
    [field:SerializeField] public float LinePrintPause { get; private set; }

    [Header("Death timer")]
    [SerializeField] private float _returnToTitleScreenOnDeathWaitTime = 5f;

    public ViewController ViewController { get; private set; }
    [field: Header("Views")]
    [field: Header("Empties")]
    [field: SerializeField] public ViewElement ViewBackgroundBlackScreen { get; private set; }
    [field: SerializeField] public ViewElement ViewEmpty { get; private set; }

    [field: Header("Transitionnal")]
    [field: SerializeField] public ViewTitleScreen ViewTitleScreen { get; private set; }
    [field: SerializeField] public ViewElementOptions ViewOptionMenu { get; private set; }
    [field: SerializeField] public ViewLoadingScreen ViewLoadingScreen { get; private set; }
    [field: SerializeField] public ViewElement ViewDeathScreen { get; private set; }
    [field: SerializeField] public ViewElement ViewWinScreen { get; private set; }

    [field: Header("HUD")]
    [field: SerializeField] public ViewPlayerSkills ViewPlayerCooldowns { get; private set; }
    [field: SerializeField] public ViewPlayerStats ViewPlayerStats { get; private set; }
    [field: SerializeField] public ViewWaveStats ViewWaveStats { get; private set; }
    [field: SerializeField] public ViewFillingBarWithTextElement ViewPlayerHealthBar { get; private set; }
    [field: SerializeField] public ViewFillingBarWithTextElement ViewPlayerCurrencyBar { get; private set; }
    [field: SerializeField] public ViewElement ViewPersistentCurrency { get; private set; }

    [field: Header("AI")]
    [field: SerializeField] public ViewBossHealthBars ViewBossHealthBars { get; private set; }

    [field: Header("World")]
    [field: SerializeField] public ViewInteract ViewInteract { get; private set; }

    [field: Header("User & Databse interaction")]
    [field: SerializeField] public ViewElement ViewLogin { get; private set; }
    [field: SerializeField] public ViewPromoCode ViewPromoCode { get; private set; }
    [field: SerializeField] public ViewShop ViewShop { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        AudioSource = GetComponent<AudioSource>();
        ViewController = GetComponent<ViewController>();
    }

    public void ShowHUD()
    {
        Entity_Player player = Entity_Player.Instance;

        ViewPlayerHealthBar.OnShow();
        ViewPlayerCurrencyBar.OnShow();
        ViewPersistentCurrency.OnShow();
        ViewPlayerCooldowns.OnShow();
        ViewPlayerStats.OnShow( () =>
        {
            player.RefreshPlayerStats();
        });

        player.RefreshHealthBar();
        player.RefreshGoldBar();
        // TODO: Update persistent currency here

        ViewWaveStats.OnShow();

    }

    public void HideHUD()
    {
        if (ViewPlayerHealthBar.gameObject.activeSelf)
        {
            ViewPlayerHealthBar.OnHideQuick();
        }

        if (ViewPlayerCurrencyBar.gameObject.activeSelf)
        {
            ViewPlayerCurrencyBar.OnHideQuick();
        }

        if (ViewPlayerCooldowns.gameObject.activeSelf)
        {
            ViewPlayerCooldowns.OnHideQuick();
        }

        if (ViewPlayerStats.gameObject.activeSelf)
        {
            ViewPlayerStats.OnHideQuick();
        }

        if (ViewInteract.gameObject.activeSelf)
        {
            ViewPlayerStats.OnHideQuick();
        }

        if (ViewWaveStats.gameObject.activeSelf)
        {
            ViewWaveStats.OnHideQuick();
        }

        if (ViewPersistentCurrency.gameObject.activeSelf)
        {
            ViewPersistentCurrency.OnHideQuick();
        }
    }

    public void TransitionToDeathScreenView()
    {
        if (ViewDeathScreen.gameObject.activeSelf) { return; }

        Entity_Player.Instance.Freeze();

        //ViewBossHealthBars.OnHideQuick();
        ViewBossHealthBars.OnHideInstantaneous();
        HideHUD();
        ViewController.SwitchViewSynchronous(ViewDeathScreen, showCallback: () =>
        {
            TimerManager.Instance.AddSequentialStopwatch(_returnToTitleScreenOnDeathWaitTime, () =>
            {
                SceneLoadManager.Instance.GoToTitleScreen();
            });
        });
    }

    public void TransitionToGameWonScreenView()
    {
        if (ViewWinScreen.gameObject.activeSelf) { return; }

        Entity_Player.Instance.Freeze();

        ViewBossHealthBars.OnHideInstantaneous();
        HideHUD();
        ViewController.SwitchViewSynchronous(ViewWinScreen, showCallback: () =>
        {
            TimerManager.Instance.AddSequentialStopwatch(_returnToTitleScreenOnDeathWaitTime, () =>
            {
                SceneLoadManager.Instance.GoToTitleScreen();
            });
        });
    }

    public void ResetCooldownView(ViewFillingBarWithTextElement view)
    {
        view.TextElement.Element.text = "";
        view.Filler.UnfillCompletely();
    }

    public void RefreshCooldownVisuals(ViewFillingBarWithTextElement view, string remainingTime, float fillingNormalized)
    {
        view.TextElement.Element.text = remainingTime;
        view.Filler.SetFilling(fillingNormalized);
    }
}
