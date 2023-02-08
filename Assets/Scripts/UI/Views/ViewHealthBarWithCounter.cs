using UnityEngine;

public class ViewHealthBarWithCounter : ViewElement
{
    [field:SerializeField] public ImageFiller Filler { get; private set; }
    [field:SerializeField] public TextMeshProElement Counter { get; private set; }
}
