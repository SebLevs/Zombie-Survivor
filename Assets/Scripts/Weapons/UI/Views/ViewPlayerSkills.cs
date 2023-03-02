using UnityEngine;

public class ViewPlayerSkills : ViewElement
{
    [field: SerializeField] public ViewFillingBarWithTextElement MainSkill { get; private set; }
    [field: SerializeField] public ViewFillingBarWithTextElement SecondarySkill { get; private set; }
    [field: SerializeField] public ViewFillingBarWithTextElement SpaceBarSkill { get; private set; }
}
