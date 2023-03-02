using UnityEngine;

public class ViewFillingBarWithTextElement : ViewElement
{
    [field:SerializeField] public ImageFiller Filler { get; private set; }
    [field:SerializeField] public TextMeshProElement TextElement { get; private set; }
}
