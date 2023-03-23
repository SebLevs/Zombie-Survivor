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
        var objectLocalizations = _localizationManager.ObjectsLocalizations;
        Languages language = _localizationManager.Language;
        _textElement.text = TSVLocalizer.GetObjectLocalizationValue(objectLocalizations, key, language);
    }

    public void SetKey(string key) => this.key = key;
    public void SetTextColor(Color color) => _textElement.color = color;
}
