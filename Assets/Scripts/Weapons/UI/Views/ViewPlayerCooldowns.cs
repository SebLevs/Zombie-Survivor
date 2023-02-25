using UnityEngine;

public class ViewPlayerCooldowns : ViewElement
{
    [field: SerializeField] public ViewFillingBarWithCounter m_mainSkill { get; private set; }
    [field: SerializeField] public ViewFillingBarWithCounter m_secondarySkill { get; private set; }
    [field: SerializeField] public ViewFillingBarWithCounter m_TertiarySkill { get; private set; }
}
