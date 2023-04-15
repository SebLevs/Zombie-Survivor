using UnityEngine;

public class Player_DeadState : State<Entity_Player>
{
    public Player_DeadState(Entity_Player context) : base(context)
    {
    }

    public override void OnEnter()
    {
        Entity_Player.Instance.Rb.velocity = Vector2.zero;
        UIManager.Instance.TransitionToDeathScreenView();
        Entity_Player.Instance.GetComponent<Animator>().enabled = false;
        Entity_Player.Instance.AutomatedTestController.enabled = false;
        Debug.Log("ENTER DEATH");
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        Entity_Player.Instance.GetComponent<Animator>().enabled = true;
    }

    public override void HandleStateTransition()
    {
    }
}
