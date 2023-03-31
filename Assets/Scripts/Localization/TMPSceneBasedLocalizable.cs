using TMPro;
using UnityEngine;

public class TMPSceneBasedLocalizable : MonoBehaviour, ILocalizationListener
{
    [SerializeField] private string key;
    [SerializeField] private SceneBasedLocalizer localizer;
    private ILocalizationObserver localizationCaller;
    private TextMeshProUGUI _textElement;

    private void Awake()
    {
        _textElement = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetLocalizationCaller();
        localizationCaller.SubscribeToLocalization(this);
        if (key != "") { LocalizeText(); }
    }

    private void SetLocalizationCaller()
    {
        if (localizer == null)
        {
            localizationCaller = LocalizationManager.Instance;
        }
        else
        {
            localizationCaller = localizer;
        }
    }

    public void LocalizeText()
    {
        var objectLocalizations = localizationCaller.GetObjectLocalizationDictionary();
        Languages language = LocalizationManager.Instance.Language;
        _textElement.text = TSVLocalizer.GetObjectLocalizationValue(objectLocalizations, key, language);
    }

    public void LocalizeExternalText(string externalKey)
    {
        var objectLocalizations = localizationCaller.GetObjectLocalizationDictionary();
        Languages language = LocalizationManager.Instance.Language;
        _textElement.text = TSVLocalizer.GetObjectLocalizationValue(objectLocalizations, externalKey, language);
    }
}
