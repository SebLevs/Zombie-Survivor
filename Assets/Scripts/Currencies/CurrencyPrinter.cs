using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyPrinter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] public UnityEvent refreshMethodChoice;
    public void Refresh() => refreshMethodChoice?.Invoke();

    private void Start()
    {
        refreshMethodChoice?.Invoke();
    }

    private void OnEnable()
    {
        refreshMethodChoice?.Invoke();
    }

    public void RefreshSmallGold()
    {
        textMeshPro.text = Entity_Player.Instance.baseStats.SmallGold.ToString();
    }

    public void RefreshBigGold()
    {
        textMeshPro.text = Entity_Player.Instance.baseStats.BigGold.ToString();
    }
}
