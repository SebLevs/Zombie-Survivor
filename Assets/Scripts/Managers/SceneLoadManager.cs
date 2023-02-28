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
        UIManager _uiManager = UIManager.Instance;
        while (async.progress < 0.95f)
        {
            yield return new WaitForFixedUpdate();
            //_uiManager.View_loadingBarLevel.FillingBarElement.SetFilling(async.progress);
        }
        yield return new WaitUntil(() => async.progress > 0.95f);
        yield return new WaitForSeconds(_minimalWaitTime);

        //_uiManager.View_loadingBarLevel.FillingBarElement.SetFilling(1.0f);
        //_uiManager.View_loadingBarLevel.OnHide();

        UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewEmpty); // TODO: Switch to HUD or something

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

        uiManager.OnSwitchViewSynchronous(uiManager.ViewBlackScreen, 
        showCallback: () =>
        {
            Entity_Player.Instance.Reinitialize();
            UnloadCurrentScene();
            uiManager.ViewBackgroundBlackScreen.OnShowQuick();
            uiManager.OnSwitchViewSynchronous(uiManager.ViewTitleScreen);
            IsInTitleScreen = true;
        });
    }
}
