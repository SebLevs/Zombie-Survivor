using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class DropDownLanguageSelection : MonoBehaviour
{
    [field:SerializeField] public TMP_Dropdown DropDownLanguages { get; private set; }
    [field:SerializeField] public TextMeshProUGUI Label { get; private set; }

    private void Start()
    {
        InitDropdownLanguageItems();
        SetValueAsCurrentLanguage();
    }

    private void InitDropdownLanguageItems()
    {
        DropDownLanguages.ClearOptions();
        foreach (Languages language in Enum.GetValues(typeof(Languages)))
        {
            TMP_Dropdown.OptionData option = new(language.ToString().ToLower().FirstCharacterToUpper());
            DropDownLanguages.options.Add(option);
        }
    }

    private void SetValueAsCurrentLanguage()
    {
        // -2 as Languages (key for localization) starts at column index 2 in TSV file
        int value = ((int)LocalizationManager.Instance.Language) - 2;
        DropDownLanguages.value = value;
        Label.text = DropDownLanguages.options[value].text;
    }

    public void Localize()
    {
        LocalizationManager localizationManager = LocalizationManager.Instance;

        // +2 as Languages (key for localization) starts at column index 2 in TSV file
        localizationManager.Language = (Languages)DropDownLanguages.value + 2;
        localizationManager.NotifyILocalizationListeners();
    }
}
