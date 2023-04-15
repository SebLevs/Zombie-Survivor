using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestButtonTextUpdater : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private const string DEV = "DEV";
    private const string AUTOPLAY = "AUTO PLAY";

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        SetButtonTextFromEnable(Entity_Player.Instance.GetComponent<PlayerAutomatedTestController>());
    }

    public void SetButtonTextFromEnable(PlayerAutomatedTestController controller)
    {
        textMeshPro.text = DEV + "\n" + AUTOPLAY + "\n";
        textMeshPro.text += controller.enabled.ToString();
    }
}
