using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPLocalizable : MonoBehaviour, ILocalizationListener
{
    [SerializeField] private string key;
    private TextMeshProUGUI _textElement;

    private void Awake()
    {
        _textElement = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        LocalizationManager.Instance.SubscribeToLocalization(this);
        if (key != "") { LocalizeText(); }
    }

    public void LocalizeText()
    {
        LocalizationManager localizationManager = LocalizationManager.Instance;
        var objectLocalizations = localizationManager.ObjectsLocalizations;
        Languages language = localizationManager.Language;
        _textElement.text = TSVLocalizer.GetObjectLocalizationValue(objectLocalizations, key, language);
    }

    public void LocalizeExternalText(string externalKey)
    {
        LocalizationManager localizationManager = LocalizationManager.Instance;
        var objectLocalizations = localizationManager.ObjectsLocalizations;
        Languages language = localizationManager.Language;
        _textElement.text = TSVLocalizer.GetObjectLocalizationValue(objectLocalizations, externalKey, language);
    }
}
