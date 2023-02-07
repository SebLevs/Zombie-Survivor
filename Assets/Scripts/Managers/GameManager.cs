using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public bool IsPaused;
    public bool IsPlayTest;

    protected override void OnStart()
    {
        base.OnStart();

        if (IsPlayTest)
        {
            InitiatePlayTest();
            return; 
        }
        
        UIManager.Instance.OnSwitchViewSequential(UIManager.Instance.ViewTitleScreen);
    }

    private void InitiatePlayTest()
    {
        // TODO: Activate any required assets for general playtest here
    }

    public void StartGame()
    {
        UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewBlackScreen, showCallback: () =>
        {
            TimerManager.Instance.AddSequentialTimer(1f, callback: () =>
            {
                UIManager.Instance.ViewBlackScreen.OnHide();
                SceneLoadManager.Instance.OnLoadScene(1);
                UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewEmpty);
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
}
