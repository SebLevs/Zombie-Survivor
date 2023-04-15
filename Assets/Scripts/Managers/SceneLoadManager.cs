using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Manager<SceneLoadManager>
{
    [field: SerializeField] public SceneData baseGameplayScene { get; private set; }

    public bool IsInTitleScreen = true;

    [SerializeField] private float _minimalWaitTime;
    private AsyncOperation async;
    private SceneData currentScene = null;

    private void UnloadCurrentScene()
    {
        if (currentScene == null) { return; }
        SceneManager.UnloadSceneAsync(currentScene.name);
        currentScene = null;
    }

    public void LoadScene(SceneData scene)
    {
        GameManager.Instance.PauseGame();
        UIManager uiManager = UIManager.Instance;
        uiManager.HideHUD();
        uiManager.ViewPromoCode.OnHideQuick();

        uiManager.ViewBackgroundBlackScreen.OnShow();
        uiManager.ViewController.SwitchViewSynchronous(uiManager.ViewLoadingScreen,
        showCallback: () =>
        {
            PrepareSceneLoad(scene);
            uiManager.ViewBackgroundBlackScreen.OnHideQuick();
        });
    }

    private void PrepareSceneLoad(SceneData scene)
    {
        UnloadCurrentScene();
        currentScene = scene;
        async = SceneManager.LoadSceneAsync(currentScene.name, LoadSceneMode.Additive);
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        UIManager uiManager = UIManager.Instance;

        uiManager.ViewLoadingScreen.Init();
        uiManager.ViewLoadingScreen.ViewSlider.OnShowQuick();

        while (async.progress < 0.95f)
        {
            yield return new WaitForFixedUpdate();
            uiManager.ViewLoadingScreen.ViewSlider.SetsliderValue(async.progress);
            yield return new WaitForSeconds(0.2f);
        }
        uiManager.ViewLoadingScreen.AnimateOnReachedEndValue();

        uiManager.ViewLoadingScreen.ViewSlider.SetsliderValue(1.0f);
        yield return new WaitForSeconds(_minimalWaitTime);

        Entity_Player.Instance.Reinitialize();

        uiManager.ViewLoadingScreen.ViewSlider.OnHideQuick( () =>
        {
            UIManager.Instance.ViewController.SwitchViewSynchronous(UIManager.Instance.ViewEmpty);
        });

        async = null;
        InitScene();
    }

    private void InitScene()
    {
        AudioManager.Instance.PlayLoopingClip(AudioManager.Instance.AmbianceClip);

        IsInTitleScreen = false;
        GameManager.Instance.ResumeGame();
        UIManager.Instance.ShowHUD();
    }

    public void GoToTitleScreen()
    {
        GameManager.Instance.PauseGame();
        UIManager uiManager = UIManager.Instance;

        uiManager.ViewBossHealthBars.OnHideQuick();
        uiManager.HideHUD();

        AudioManager.Instance.StopPlayingLoopingClip();

        uiManager.ViewController.SwitchViewSynchronous(uiManager.ViewLoadingScreen, 
        showCallback: () =>
        {
            UnloadCurrentScene();
            System.GC.Collect();
            uiManager.ViewBackgroundBlackScreen.OnShowQuick();

            uiManager.ViewController.SwitchViewSynchronous(uiManager.ViewTitleScreen, 
            showCallback: () =>
            {
                uiManager.ViewPromoCode.TryShowView();
                Entity_Player.Instance.Reinitialize();
            });

            IsInTitleScreen = true;
        });
    }
}
