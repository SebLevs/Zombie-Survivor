using System.Collections.Generic;
using UnityEngine;

// TODO: IF TIME: Refactor language selection to associate a specific FontAsset on a per language basis
// ... Currently uses Noto Simplified chinese which allows for all currently used languages (roman-based, japanese, chinese)

public class LocalizationManager : Manager<LocalizationManager>, IPlayerPrefHandler, ILocalizationObserver
{
    private const string _playerPrefKey = "Language";
    private const string _pathTsvUIDefaults = "tsv_UI_Defaults.txt";

    [Header("Language")]
    public Languages Language = Languages.ENGLISH;

    public string[] ObjectLocalizationHeaders;
    public Dictionary<string, ObjectLocalizations> ObjectsLocalizations;

    private HashSet<ILocalizationListener> _localizationListeners;

    private HashSet<ILocalizationObserver> _sceneBasedLocalizerListeners;

    protected override void OnAwake()
    {
        base.OnAwake();
        ObjectsLocalizations = new();
        _localizationListeners = new();
        _sceneBasedLocalizerListeners = new();
        ObjectLocalizationHeaders = TSVLocalizer.GetHeadersAsString(_pathTsvUIDefaults, 2);
        TSVLocalizer.SetTranslationDatasFromFile(ObjectsLocalizations, _pathTsvUIDefaults, 1, 1);
        LoadFromPlayerPref();
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

    public void SubscribeToLocalizer(ILocalizationObserver localizationListener)
    {
        _sceneBasedLocalizerListeners.Add(localizationListener);
    }

    public void UnSubscribeFromLocalizer(ILocalizationObserver localizationListener)
    {
        _sceneBasedLocalizerListeners.Remove(localizationListener);
    }

    public void NotifyILocalizerListeners()
    {
        foreach (ILocalizationObserver listener in _sceneBasedLocalizerListeners)
        {
            listener.NotifyLocalizationListeners();
        }
    }

    public void AddLocalizationsToDictionary(Dictionary<string, ObjectLocalizations> newlocalizations)
    {
        foreach (var item in newlocalizations)
        {
            ObjectsLocalizations!.Add(item.Key, item.Value);
        }
    }

    public void SaveToPlayerPref()
    {
        Languages lastSavedLanguage = (Languages)PlayerPrefs.GetInt(_playerPrefKey);
        if (lastSavedLanguage != Language)
        {
            PlayerPrefs.SetInt(_playerPrefKey, (int)Language);
        }
    }

    public void LoadFromPlayerPref()
    {
        Language = (PlayerPrefs.GetInt(_playerPrefKey) == 0) ? Languages.ENGLISH : (Languages)PlayerPrefs.GetInt(_playerPrefKey);
    }

    public Dictionary<string, ObjectLocalizations> GetObjectLocalizationDictionary()
    {
        return ObjectsLocalizations;
    }
}
