using System.Collections.Generic;

public interface ILocalizationObserver
{
    public Dictionary<string, ObjectLocalizations> GetObjectLocalizationDictionary();

    public void SubscribeToLocalization(ILocalizationListener localizationListener);

    public void UnSubscribeFromLocalization(ILocalizationListener localizationListener);

    public void NotifyLocalizationListeners();
}
