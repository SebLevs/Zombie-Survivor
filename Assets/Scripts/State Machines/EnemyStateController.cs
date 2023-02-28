using System.Collections;
using UnityEngine;

public abstract class EnemyStateController : MonoBehaviour, IFrameUpdateListener
{
    [field:Header("Reaction colliderActiveTime: Used for attack, state transitions, etc.")]
    [field:Tooltip(
        "Human reaction colliderActiveTime in seconds (average): \n" +
        "Visual stimulus: ~0.25\n" + 
        "Audio stimulus: ~0.17\n" +
        "Touch stimulus: ~0.15\n")]
    [field:Min(0)][field:SerializeField] public float ReactionTime { get; private set; }

    public Enemy Context { get; private set; }
    public EnemyState CurrentState { get; private set; }

    [SerializeField] private float _returnToDefaultAftertime = 5f;

    public SequentialTimer ReturnToDefaultStatetimer { get; private set; }

    private void Awake()
    {
        Init(GetComponent<Enemy>());
    }

    public virtual void Init(Enemy context)
    {
        ReturnToDefaultStatetimer = new SequentialTimer(_returnToDefaultAftertime, () => SetStateAsDefault());
        Context = context;
        InitStates();
        CurrentState = GetDefaultState();
        CurrentState.OnEnter();
    }

    protected abstract void InitStates();
    public abstract EnemyState GetDefaultState();

    public void SetStateAsDefault()
    {
        OnTransitionState(GetDefaultState());
    }

    public void OnUpdate()
    {
        CurrentState.OnUpdate();
        CurrentState.HandleStateTransition();
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
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

    /// <param name="rangeModifier">
    /// Used to get a random rangeModifier between ReactionTime - rangeModifier and ReactionTime + rangeModifier
    /// </param>
    public float GetReactionTimeInRange(float rangeModifier)
    {
        float rangedReactionTime = Random.Range(ReactionTime * ( 1 - rangeModifier), ReactionTime * (1 + rangeModifier));
        return rangedReactionTime;
    }

    public IEnumerator DelayedAnimatorTrigger(int attackAnimHash, float rangeModifier)
    {
        yield return new WaitForSeconds(GetReactionTimeInRange(rangeModifier));
        Context.PathfinderUtility.DisablePathfinding();
        Context.Animator.SetTrigger(attackAnimHash);
    }

    public bool IsInAnimationState(AnimationClip clip)
    {
        return Context.Animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name);
    }

    public abstract void TransitionToDeadState();
}
