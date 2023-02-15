using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State<EnemyStateController>
{
    protected EnemyState(EnemyStateController controller) : base(controller)
    {
    }
}
