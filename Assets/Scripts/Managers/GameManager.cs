using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public bool IsPaused { get; private set; }

    [Header("Play test setup")]
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

        UIManager.Instance.ViewBackgroundBlackScreen.gameObject.SetActive(true);
        SceneLoadManager.Instance.GoToTitleScreen();
    }

    public void SetCursorLockState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    private void InitiatePlayTest()
    {
        Debug.LogWarning($"Play test started on scene: {_playTestScene}");
        SceneLoadManager.Instance.OnLoadScene(_playTestScene);
        UIManager.Instance.ViewBackgroundBlackScreen.OnHide();
        UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewEmpty); // TODO: Switch to HUD or something
    }

    public void StartGame()
    {
        UIManager.Instance.ViewBackgroundBlackScreen.OnHide();
        UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewBlackScreen, 
        showCallback: () =>
        {
            SceneLoadManager.Instance.OnLoadScene(1);
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
        NotifyPauseListenersOnResume();
        //CommandPromptManager.Instance.DeActivate();
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
