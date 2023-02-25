using UnityEngine;

public class ViewBossHealthBars : ViewElement
{
    [field: SerializeField] public ViewFillingBarWithCounter ViewBossHealthBar { get; private set; }

    public ViewFillingBarWithCounter InitBossHealthBar(ViewFillingBarWithCounter bar, EnemyType type)
    {
        bar.Filler.FillUpCompletely();
        bar.Counter.Element.text = type.TypeName;
        return ViewBossHealthBar;
    }
}
