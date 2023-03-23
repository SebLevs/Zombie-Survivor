using System;
using TMPro;
using UnityEngine;

[Serializable]
public class KeyColorPair
{
    [field:SerializeField] public string Key { get; private set; }
    [field:SerializeField] public Color Color { get; private set; }
}

public class TMPLocalizablePair : MonoBehaviour, ILocalizationListener
{
    [field:SerializeField] public KeyColorPair PrimaryPair { get; private set; }
    [field:SerializeField] public KeyColorPair SecondaryPair { get; private set; }

    private KeyColorPair _currentPair;
    private TextMeshProUGUI _textElement;
    private LocalizationManager _localizationManager;

    private void Awake()
    {
        _textElement = GetComponent<TextMeshProUGUI>();
        _currentPair = PrimaryPair;
        _localizationManager = LocalizationManager.Instance;
        _localizationManager.SubscribeToLocalization(this);
    }

    private void Start()
    {
        LocalizeText();
    }

    public void SetPair(KeyColorPair pair) => _currentPair = pair;

    public void LocalizeText()
    {
        var objectLocalizations = _localizationManager.ObjectsLocalizations;
        Languages language = _localizationManager.Language;
        _textElement.color = _currentPair.Color;
        _textElement.text = TSVLocalizer.GetObjectLocalizationValue(objectLocalizations, _currentPair.Key, language);
    }
}
