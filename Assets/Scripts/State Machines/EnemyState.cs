using System;
[Serializable]
public abstract class EnemyState : State<EnemyStateController>
{
    protected EnemyState(EnemyStateController controller) : base(controller)
    {
    }

    public abstract bool IsTransitionValid();
}
