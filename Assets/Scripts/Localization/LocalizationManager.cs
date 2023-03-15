using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : Manager<LocalizationManager>
{
    private const string _pathTsvUIDefaults = "tsv_UI_Defaults.txt";

    [Header("Language")]
    public Languages Language = Languages.ENGLISH;

    public Dictionary<string, ObjectLocalizations> ObjectsLocalizations;

    private HashSet<ILocalizationListener> _localizationListeners;

    protected override void OnAwake()
    {
        base.OnAwake();
        ObjectsLocalizations = new();
        _localizationListeners = new();
        TSVLocalizer.SetTranslationDatasFromFile(ObjectsLocalizations, _pathTsvUIDefaults, 1, 1);
    }

    public void SubscribeToLocalization(ILocalizationListener frameUpdateListener)
    {
        _localizationListeners.Add(frameUpdateListener);
    }

    public void UnSubscribeFromLocalizatioon(ILocalizationListener frameUpdateListener)
    {
        _localizationListeners.Remove(frameUpdateListener);
    }

    public void NotifyILocalizationListeners()
    {
        foreach (ILocalizationListener listener in _localizationListeners)
        {
            listener.LocalizeText();
        }
    }

    public void AddLocalizationsToDictionary(Dictionary<string, ObjectLocalizations> newlocalizations)
    {
        foreach (var item in newlocalizations)
        {
            ObjectsLocalizations!.Add(item.Key, item.Value);
        }
    }
}
