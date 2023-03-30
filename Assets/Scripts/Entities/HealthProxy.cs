using UnityEngine;

public class HealthProxy : Health
{
    [SerializeField] private Health proxy;

    [SerializeField][Min(0)] private int _damageModifier = 1;

    public override void Hit(int damage)
    {
        damage *= _damageModifier;
        base.Hit(damage);
        proxy.Hit(damage);
    }

    public void HealProxyClamped()
    {
        CurrentHP = (proxy.CurrentHP < MaxHP) ? proxy.CurrentHP : MaxHP;
    }
}
