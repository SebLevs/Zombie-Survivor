using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour, ILocalizerListener
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

    public void SubscribeToLocalization(ILocalizationListener localizationListener)
    {
        _localizationListeners.Add(localizationListener);
    }

    public void UnSubscribeFromLocalizatioon(ILocalizationListener localizationListener)
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
}
