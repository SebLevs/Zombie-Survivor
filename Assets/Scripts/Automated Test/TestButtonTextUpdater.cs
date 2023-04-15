using TMPro;
using UnityEngine;

public class TestButtonTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private const string DEV = "DEV";
    private const string AUTOPLAY = "AUTO PLAY";

    private void OnEnable()
    {
        SetButtonTextFromEnable(Entity_Player.Instance.AutomatedTestController);
    }

    public void SetButtonTextFromEnable(PlayerAutomatedTestController controller)
    {
        textMeshPro.text = DEV + "\n" + AUTOPLAY + "\n";
        textMeshPro.text += controller.enabled.ToString();
    }
}
