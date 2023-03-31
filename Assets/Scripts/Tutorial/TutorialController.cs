using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour, ILocalizerListener, ILocalizationCaller
{
    [Header("Boss")]
    [SerializeField] private GameObject bossObject;

    [Header("Tutorial Views")]
    [SerializeField] private float cueDuration = 5;
    [SerializeField] private ViewController viewController;
    [SerializeField] private List<ViewElement> tutorialViewsInOrder;

    private Entity_Player _player;

    private const string _pathTsvUIDefaults = "tsv_UI_Tutorial.txt";
    public string[] ObjectLocalizationHeaders;
    public Dictionary<string, ObjectLocalizations> ObjectsLocalizations;

    private HashSet<ILocalizationListener> _localizationListeners;

    private void OnEnable()
    {
        LocalizationManager.Instance.SubscribeToLocalizer(this);
    }

    private void OnDisable()
    {
        LocalizationManager localizationManager = LocalizationManager.Instance;
        if (localizationManager)
        {
            localizationManager.UnSubscribeFromLocalizer(this);
        }
    }

    private void Awake()
    {
        ObjectsLocalizations = new();
        _localizationListeners = new();
        ObjectLocalizationHeaders = TSVLocalizer.GetHeadersAsString(_pathTsvUIDefaults, 2);
        TSVLocalizer.SetTranslationDatasFromFile(ObjectsLocalizations, _pathTsvUIDefaults, 1, 1);
    }

    private void Start()
    {
        TimerManager.Instance.AddSequentialTimer(0.0f, () => 
        {
            GameManager.Instance.PauseGame();
        });

        _player = Entity_Player.Instance;
        _player.arrow.SetTargetAs(bossObject.transform);
        StartCoroutine(ShowTutorialTexts());
    }

    public void OnTutorialCompletion()
    {
        _player.UserDatas.hasCompletedTutorial = true;
    }

    public IEnumerator ShowTutorialTexts()
    {
        _player.Input.enabled = false;
        yield return new WaitForSeconds(cueDuration);

        for (int i = 0; i < tutorialViewsInOrder.Count - 1; i++)
        {
            viewController.SwitchViewSequential(tutorialViewsInOrder[i]);
            yield return new WaitForSeconds(cueDuration);
        }

        viewController.CurrentView.OnHideInstantaneous();

        CinemachineVirtualCamera cam = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
        cam.Follow = bossObject.transform;
        cam.LookAt = bossObject.transform;
        ViewElement viewPortal = tutorialViewsInOrder[tutorialViewsInOrder.Count - 1];
        viewController.SwitchViewSequential(viewPortal);
        yield return new WaitForSeconds(cueDuration);

        cam.Follow = _player.transform;
        cam.LookAt = _player.transform;
        viewController.CurrentView.OnHide();
        GameManager.Instance.ResumeGame();
        _player.Input.enabled = true;
    }

    public void LoadScene(SceneData scene)
    {
        SceneLoadManager.Instance.LoadScene(scene);
    }

    public void SubscribeToLocalization(ILocalizationListener localizationListener)
    {
        _localizationListeners.Add(localizationListener);
    }

    public void UnSubscribeFromLocalization(ILocalizationListener localizationListener)
    {
        _localizationListeners.Remove(localizationListener);
    }

    public void LocalizeLocalizationListeners()
    {
        foreach (ILocalizationListener listener in _localizationListeners)
        {
            listener.LocalizeText();
        }
    }

    public Dictionary<string, ObjectLocalizations> GetObjectLocalizationDictionary()
    {
        return ObjectsLocalizations;
    }
}
