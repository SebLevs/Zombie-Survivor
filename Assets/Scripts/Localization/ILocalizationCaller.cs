using System.Collections.Generic;

public interface ILocalizationCaller
{
    public Dictionary<string, ObjectLocalizations> GetObjectLocalizationDictionary();
}
