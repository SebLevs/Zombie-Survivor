using UnityEngine;

public abstract class EnemyStateController : MonoBehaviour, IFrameUpdateListener
{
    public Enemy Context { get; private set; }
    public EnemyState CurrentState { get; private set; }

    private void Awake()
    {
        Init(GetComponent<Enemy>());
    }

    public virtual void Init(Enemy context)
    {
        Context = context;
        InitStates();
        CurrentState = GetDefaultState();
        CurrentState.OnEnter();
    }

    protected abstract void InitStates();
    protected abstract EnemyState GetDefaultState();

    public void OnUpdate()
    {
        CurrentState.OnUpdate();
        CurrentState.HandleStateTransition();
    }

    public void OnDisable()
    {
        UpdateManager.Instance.UnSubscribeFromUpdate(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    /// <summary>
    /// 1 - Exit current state<br/>
    /// 2 - Set and enter parameter state
    /// </summary>
    public void OnTransitionState(EnemyState state)
    {
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
