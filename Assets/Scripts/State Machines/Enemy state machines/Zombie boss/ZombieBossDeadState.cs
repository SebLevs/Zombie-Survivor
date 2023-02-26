using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBossDeadState : EnemyState
{
    public ZombieBossDeadState(EnemyStateController controller) : base(controller)
    {
    }

    public override void HandleStateTransition()
    {
    }

    public override bool IsTransitionValid() { return m_controller.Context.Health.IsDead; }

    public override void OnEnter()
    {
        SceneLoadManager.Instance.GoToTitleScreen();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
