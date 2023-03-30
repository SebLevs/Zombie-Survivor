using System.Collections;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Boss")]
    [SerializeField] private GameObject bossObject;

    [Header("Tutorial Views")]
    [SerializeField] private float cueDuration = 5;
    [SerializeField] private ViewController viewController;
    [SerializeField] private ViewElement waveView;
    [SerializeField] private ViewElement chestView;
    [SerializeField] private ViewElement potionView;

    private Entity_Player _player;

    private void Start()
    {
        _player = Entity_Player.Instance;
        _player.arrow.SetTargetAs(bossObject.transform);
        //StartCoroutine(ShowTutorialTexts());
    }

    public void OnTutorialCompletion()
    {
        _player.Reinitialize();
        _player.UserDatas.hasCompletedTutorial = true;
    }

    public IEnumerator ShowTutorialTexts()
    {
        GameManager.Instance.PauseGame();
        viewController.SwitchViewSequential(waveView);
        yield return new WaitForSeconds(cueDuration);
        viewController.SwitchViewSequential(chestView);
        yield return new WaitForSeconds(cueDuration);
        viewController.SwitchViewSequential(potionView);
        yield return new WaitForSeconds(cueDuration);
        GameManager.Instance.ResumeGame();
    }

    public void LoadScene(SceneData scene)
    {
        SceneLoadManager.Instance.LoadScene(scene);
    }
}
