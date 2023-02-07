using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public bool IsPaused;

    protected override void OnStart()
    {
        base.OnStart();
        UIManager.Instance.OnSwitchViewSequential(UIManager.Instance.ViewTitleScreen);
    }

    public void StartGame()
    {
        UIManager.Instance.OnSwitchViewSynchronous(UIManager.Instance.ViewBlackScreen, showCallback: () =>
        {
            TimerManager.Instance.AddSequentialTimer(1f, callback: () =>
            {
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!UIManager.Instance.ViewOptionMenu.gameObject.activeSelf)
            {
                UIManager.Instance.OnSwitchViewSequential(UIManager.Instance.ViewOptionMenu);
            }
            else
            {
                UIManager.Instance.OnSwitchViewSequential(UIManager.Instance.ViewEmpty);
            }
        }
    }
}
