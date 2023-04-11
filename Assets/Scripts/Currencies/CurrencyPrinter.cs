using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyPrinter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] public UnityEvent refreshMethodChoice;
    public void Refresh() => refreshMethodChoice?.Invoke();

    private void OnEnable()
    {
        refreshMethodChoice?.Invoke();
    }

    public void RefreshSmallGold()
    {
        Debug.Log("Small gold refresh was called for  SMALL GOLD | Please double click me and update with proper value");
        textMeshPro.text = Entity_Player.Instance.baseStats.SmallGold.ToString();
    }

    public void RefreshBigGold()
    {
        Debug.Log("Small gold refresh was called for  BIG GOLD | Please double click me and update with proper value");
        textMeshPro.text = Entity_Player.Instance.baseStats.BigGold.ToString();
    }
}
