using UnityEngine;

public class ViewPlayerCooldowns : ViewElement
{
    [field: SerializeField] public ViewFillingBarWithCounter MainSkill { get; private set; }
    [field: SerializeField] public ViewFillingBarWithCounter SecondarySkill { get; private set; }
    [field: SerializeField] public ViewFillingBarWithCounter SpaceBarSkill { get; private set; }
}
