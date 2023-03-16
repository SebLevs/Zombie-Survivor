using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Manager<SceneLoadManager>
{
    public bool IsInTitleScreen = true;

    // using UnityEditor;
    // [SerializeField] private SceneAsset _levelOne; // Will prevent build because it uses unity editor | use a Scriptable object
    [SerializeField] private float _minimalWaitTime;
    private AsyncOperation async;
    private int currentScene = -1;

    // TODO: Make a scriptable object for current scene which can contain scripts specifying additive scenes when necesarry
    // ... Refer to that current scene to unloadCurrentScene and UnloadAdditiveScene
    public void UnloadCurrentScene()
    {
        if (currentScene == -1) { return; }
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = -1;
    }

    public void UnloadAdditiveScene(int scene)
    {
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public void OnLoadScene(string scene)
    {
        currentScene = SceneManager.GetSceneByName(scene).buildIndex;
        async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        //async.allowSceneActivation = false;
        StartCoroutine(LoadAsync());
    }

    public void OnLoadScene(int scene)
    {
        currentScene = scene;
        async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        //async.allowSceneActivation = false;
        StartCoroutine(LoadAsync());
        //CurrentScene = scene;
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

        uiManager.ViewLoadingScreen.ViewSlider.OnHideQuick( () =>
        {
            UIManager.Instance.ViewController.SwitchViewSynchronous(UIManager.Instance.ViewEmpty);
        });

        //async.allowSceneActivation = true;
        async = null;
        InitScene();
    }

    public void InitScene()
    {
        // TODO: Delete if SceneController.cs is implemented in the scope of the project
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

        // TODO: Delete if SceneController.cs is implemented in the scope of the project
        AudioManager.Instance.StopPlayingLoopingClip();

        uiManager.ViewController.SwitchViewSynchronous(uiManager.ViewLoadingScreen, 
        showCallback: () =>
        {
            Entity_Player.Instance.Reinitialize();
            UnloadCurrentScene();
            System.GC.Collect();
            uiManager.ViewBackgroundBlackScreen.OnShowQuick();
            uiManager.ViewController.SwitchViewSynchronous(uiManager.ViewTitleScreen);
            IsInTitleScreen = true;
        });
    }
}
