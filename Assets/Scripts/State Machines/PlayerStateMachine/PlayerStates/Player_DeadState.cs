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
        Entity_Player.Instance.GetComponent<PlayerAutomatedTestController>().enabled = false;
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        Debug.Log("Exit death");
        Entity_Player.Instance.GetComponent<Animator>().enabled = true;
        Entity_Player.Instance.GetComponent<PlayerAutomatedTestController>().enabled = true;
    }

    public override void HandleStateTransition()
    {
    }
}
