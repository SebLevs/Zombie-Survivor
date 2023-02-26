using UnityEngine;

public class ViewPlayerStats : ViewElement
{
    [field: SerializeField] public TextMeshProElement Invincibility { get; private set; }
    [field: SerializeField] public TextMeshProElement MoveSpeed { get; private set; }
    [field: SerializeField] public TextMeshProElement AttackCooldown { get; private set; }
    [field: SerializeField] public TextMeshProElement BoomerangCooldown { get; private set; }
    [field: SerializeField] public TextMeshProElement BoomerangDistance { get; private set; }
    [field: SerializeField] public TextMeshProElementChestStats ChestBonusPopup { get; private set; }
}
