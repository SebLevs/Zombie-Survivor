using UnityEngine;


public class ZombieBossHealthProxy : Health
{
    [SerializeField] private Enemy proxy;

    [SerializeField] [Min(0)] private int _damageModifier = 1;

    public override void Hit(int damage)
    {
        damage *= _damageModifier;
        base.Hit(damage);
        proxy?.Health.Hit(damage);
    }

    public void HealProxyClamped()
    {
        CurrentHP = (proxy.Health.CurrentHP < MaxHP) ? proxy.Health.CurrentHP : MaxHP;
    }
}