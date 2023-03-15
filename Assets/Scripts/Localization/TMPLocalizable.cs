using TMPro;
using UnityEngine;

public class TMPLocalizable : MonoBehaviour, ILocalizationListener
{
    [SerializeField] private string key;
    private TextMeshProUGUI _textElement;
    private LocalizationManager _localizationManager;

    private void Awake()
    {
        _textElement = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _localizationManager = LocalizationManager.Instance;
        _localizationManager.SubscribeToLocalization(this);
        LocalizeText();
    }

    public void LocalizeText()
    {
        _textElement.text = TSVLocalizer.GetLocalizationValue(_localizationManager.ObjectsLocalizations, key, _localizationManager.Language);
    }
}
