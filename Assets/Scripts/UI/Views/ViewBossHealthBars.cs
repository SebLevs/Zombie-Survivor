using UnityEngine;

public class ViewBossHealthBars : ViewElement
{
    [field: SerializeField] public ViewFillingBarWithTextElement ViewBossHealthBar { get; private set; }

    public ViewFillingBarWithTextElement InitBossHealthBar(ViewFillingBarWithTextElement bar, EnemyType type)
    {
        bar.Filler.FillUpCompletely();
        bar.TextElement.Element.text = type.TypeName;
        return ViewBossHealthBar;
    }
}
