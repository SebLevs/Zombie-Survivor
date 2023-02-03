using UnityEngine;

public class StateController<T>
{
    private State<T> m_currentState;

    public State<T> CurrentState { get => m_currentState; }

    public StateController(State<T> m_initialState)
    {
        m_currentState = m_initialState;
        m_currentState.OnEnter();
    }

    /// <summary>
    /// 1 - Exit current state<br/>
    /// 2 - Set and enter parameter state
    /// </summary>
    public void OnTransitionState(State<T> state)
    {
        m_currentState.OnExit();
        m_currentState = state;
        m_currentState.OnEnter();
    }

    public void OnUpdate()
    {
        m_currentState.OnUpdate();
        m_currentState.HandleStateTransition();
    }
}
