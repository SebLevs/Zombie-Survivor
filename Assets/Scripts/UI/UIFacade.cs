using UnityEngine;

public class UIFacade : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoadManager sceneLoadManager = SceneLoadManager.Instance;
        SceneLoadManager.Instance.LoadScene(sceneLoadManager.baseGameplayScene);
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

    public void StartWithScene(SceneData scene)
    {
        SceneLoadManager.Instance.LoadScene(scene);
    }

    public void GoToShop()
    {
        UIManager.Instance.ViewController.SwitchViewSequential(UIManager.Instance.ViewShop);
    }
}
