using UnityEngine;

public class SceneLoadController : MonoBehaviour
{
    [SerializeField] private SceneData tutorialScene;
    [SerializeField] private SceneData gameplayScene;

    public void StartGame()
    {
        Entity_Player player = Entity_Player.Instance;

        if (!player.UserDatas.userDatasGameplay.hasCompletedTutorial)
        {
            SceneLoadManager.Instance.LoadScene(tutorialScene);
            return;
        }

        SceneLoadManager.Instance.LoadScene(gameplayScene);
    }

    public void LoadScene(SceneData scene) => SceneLoadManager.Instance.LoadScene(scene);
}
