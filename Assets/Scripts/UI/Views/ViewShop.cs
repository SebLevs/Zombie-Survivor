using UnityEngine;

public class ViewShop : ViewElement
{
    [field: SerializeField] public CurrencyConverter CurrencyConverter { get; private set; }
    [field:SerializeField] public CurrencyPrinter SmallGold { get; private set; }
    [field:SerializeField] public CurrencyPrinter BigGold { get; private set; }

    public void CurrenciesRefresherShorthand()
    {
        SmallGold.Refresh();
        BigGold.Refresh();
    }
}
