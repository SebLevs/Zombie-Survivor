using UnityEngine;

public class ViewCurrencyPersistent : ViewElement
{
    [field: SerializeField] public TextMeshProElement CurrencyTextMeshPro { get; private set; }
    [field: SerializeField] public CurrencyPrinter CurrencyPrinter { get; private set; }
}
