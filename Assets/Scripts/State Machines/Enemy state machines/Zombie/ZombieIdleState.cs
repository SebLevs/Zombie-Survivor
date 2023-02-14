using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : EnemyState
{
    public ZombieIdleState(StateControllerZombie context) : base(context)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Entered idle state for Zombie");
    }

    public override void OnUpdate()
    {
        Debug.Log("Updated idle state for Zombie");
    }

    public override void OnExit()
    {
        Debug.Log("Exit idle state for Zombie");
    }

    public override void HandleStateTransition()
    {
        Debug.Log("Handle State Transition for Zombie");
    }
}
