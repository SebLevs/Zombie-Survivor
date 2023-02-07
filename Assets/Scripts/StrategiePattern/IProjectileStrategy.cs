using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IProjectileStrategy : ScriptableObject
{
    public abstract void Execute(Projectile projectile);
    public abstract void OnGetFromAvailable(Projectile projectile = null);
    public abstract void OnReturnToAvailable(Projectile projectile = null);
}
