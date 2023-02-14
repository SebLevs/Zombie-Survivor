using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction
{
    public PlayerActionsType actionType;
    private float _mCurrentLifeTime = 0.00f;
    private const float MAX_LIFETIME = 0.08f;

    public PlayerAction(PlayerActionsType _actionType)
    {
        this.actionType = _actionType;
    }
    public void GetOlder()
    {
        this._mCurrentLifeTime += Time.deltaTime;
    }
    public bool IsTooOld()
    {
        return this._mCurrentLifeTime >= MAX_LIFETIME;
    }
}

