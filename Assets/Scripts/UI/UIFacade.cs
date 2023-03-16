using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFacade : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void GoToTitleScreen()
    {
        SceneLoadManager.Instance.GoToTitleScreen();
    }

    public void OpenOptionMenu()
    {
        UIManager.Instance.ViewController.SwitchViewSequential(UIManager.Instance.ViewOptionMenu);
    }

    public void UnloadCurrentScene()
    {
        SceneLoadManager.Instance.UnloadCurrentScene();
    }
}
