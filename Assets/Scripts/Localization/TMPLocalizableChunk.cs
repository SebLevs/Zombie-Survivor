using System.Collections.Generic;
using UnityEngine;

public class TMPLocalizableChunk : TextMeshProElement, ILocalizationListener
{
    private string key;
    public string _currentText { get; private set; }

    [Header("Text")]
    [SerializeField] private TextAsset _localizations;
    private Dictionary<string, ObjectLocalizations> ObjectsLocalizations;
    private LocalizationManager _localizationManager;

    private void Start()
    {
        _localizationManager = LocalizationManager.Instance;
        _localizationManager.SubscribeToLocalization(this);
        TSVLocalizer.SetTranslationDatasFromFile(ObjectsLocalizations, _localizations, 1, 1);
        LocalizeText();
    }

    public void SetKey(string key) => this.key = key;

    public void LocalizeText()
    {
        _currentText = TSVLocalizer.GetObjectLocalizationValue(_localizationManager.ObjectsLocalizations, key, _localizationManager.Language);
    }

    public void SetKeyAndLocalizeText(string key)
    {
        SetKey(key);
        LocalizeText();
    }

    // TODO: Place into a printer class
    public void Print(CharacterDecorator characterDecorator)
    {

    }
}

public class CharacterDecorator
{
    private CharacterDecorator _characterDecorator;

    // TODO: Add character to decorate as parameter
    public CharacterDecorator(CharacterDecorator characterDecorator = null)
    {
        _characterDecorator = characterDecorator;
    }
}
