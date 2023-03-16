using System;
using System.Collections.Generic;

[Serializable]
public class ObjectLocalizations
{
    public ObjectLocalizations()
    {
        Localizations = new();
    }

    public Dictionary<Languages, string> Localizations;
}
