using System.Collections.Generic;
using UnityEngine;

public class SceneBasedLocalizer : MonoBehaviour, ILocalizationObserver
{
    [SerializeField] private string txtFileName = "tsv_UI_Tutorial";
    private const string _extension = ".txt";
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
        ObjectLocalizationHeaders = TSVLocalizer.GetHeadersAsString(txtFileName + _extension, 2);
        TSVLocalizer.SetTranslationDatasFromFile(ObjectsLocalizations, txtFileName + _extension, 1, 1);
    }

    public void SubscribeToLocalization(ILocalizationListener localizationListener)
    {
        _localizationListeners.Add(localizationListener);
    }

    public void UnSubscribeFromLocalization(ILocalizationListener localizationListener)
    {
        _localizationListeners.Remove(localizationListener);
    }

    public void NotifyLocalizationListeners()
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
