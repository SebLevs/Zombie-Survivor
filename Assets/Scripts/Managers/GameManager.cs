using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public bool IsPaused { get; private set; }

    [Header("PlayRandom test setup")]
    [SerializeField] private bool _isPlayTest;
    [Min(1)][SerializeField] private int _playTestScene = 1; // TODO: Refactor into a scene scriptable object for easier testing
    private HashSet<IPauseListener> _pauseListeners;

    protected override void OnAwake()
    {
        base.OnAwake();
        _pauseListeners = new HashSet<IPauseListener>();
    }

    protected override void OnStart()
    {
        base.OnStart();

        SetCursorLockState(CursorLockMode.Confined);

        if (_isPlayTest)
        {
            InitiatePlayTest();
            return; 
        }

        UIManager uiManager = UIManager.Instance;
        uiManager.ViewBackgroundBlackScreen.gameObject.SetActive(true);
        uiManager.ViewController.SwitchViewSequential(uiManager.ViewLogin);
        PauseGame();
    }

    public void SetCursorLockState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    private void InitiatePlayTest()
    {
        Debug.LogWarning($"PLAY TEST: Started on scene: {_playTestScene}");
        SceneLoadManager.Instance.OnLoadScene(_playTestScene);

        UIManager uiManager = UIManager.Instance;
        uiManager.ViewBackgroundBlackScreen.OnHide();
        uiManager.ShowHUD();
        uiManager.ViewController.SwitchViewSynchronous(UIManager.Instance.ViewEmpty);
        Entity_Player.Instance.UserDatas = new(); // fake a login to prevent null reference checks
    }

    public void StartGame()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ViewPromoCode.OnHideQuick();
        uiManager.ViewController.SwitchViewSynchronous(UIManager.Instance.ViewLoadingScreen,
        showCallback: () =>
        {
            SceneLoadManager.Instance.OnLoadScene(1);
            uiManager.ViewBackgroundBlackScreen.OnHideQuick();
        });
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
        if (IsPaused) { return; }

        IsPaused = true;
        NotifyPauseListenersOnPause();
    }

    public void ResumeGame()
    {
        if (!IsPaused) { return; }

        IsPaused = false;
        Entity_Player.Instance.DesiredActions.PurgeAllAction();
        NotifyPauseListenersOnResume();
    }

    public void SubscribeToPauseGame(IPauseListener pauseListener)
    {
        _pauseListeners.Add(pauseListener);
    }

    public void UnSubscribeFromPauseGame(IPauseListener pauseListener)
    {
        _pauseListeners.Remove(pauseListener);
    }

    public void NotifyPauseListenersOnPause()
    {
        foreach (IPauseListener pauseListener in _pauseListeners)
        {
            pauseListener.OnPauseGame();
        }
    }

    public void NotifyPauseListenersOnResume()
    {
        foreach (IPauseListener pauseListener in _pauseListeners)
        {
            pauseListener.OnResumeGame();
        }
    }
}
