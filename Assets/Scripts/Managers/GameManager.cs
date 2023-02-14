using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public bool IsPaused { get; private set; }

    [Header("Play test setup")]
    [SerializeField] private bool _isPlayTest;
    [Min(1)][SerializeField] private int _playTestScene = 1; // TODO: Refactor into a scene scriptable object for easier testing

    protected override void OnStart()
    {
        base.OnStart();

        SetCursorLockState(CursorLockMode.Confined);

        if (_isPlayTest)
        {
            InitiatePlayTest();
            return; 
        }

        UIManager.Instance.ViewBackgroundBlackScreen.OnShow();
        UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewTitleScreen);
    }

    public void SetCursorLockState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    private void InitiatePlayTest()
    {
        Debug.Log($"Play test started on scene: {_playTestScene}");
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
            TimerManager.Instance.AddSequentialTimer(1f, callback: () =>
            {
                SceneLoadManager.Instance.OnLoadScene(1);
                UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewEmpty); // TODO: Switch to HUD or something
            });
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
        IsPaused = true;
        EnemyManager.Instance.PauseCurrentlyActiveEnemies();
    }

    public void UnPauseGame()
    {
        IsPaused = false;
        EnemyManager.Instance.UnPauseCurrentlyActiveEnemies();
    }
}
