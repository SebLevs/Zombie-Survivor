using UnityEngine;

public class Player_DeadState : State<Entity_Player>
{
    public Player_DeadState(Entity_Player context) : base(context)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Dead State");
        //GameManager.Instance.PauseGame();

        UIManager.Instance.DeathTransition();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override void HandleStateTransition()
    {
        
    }
}
