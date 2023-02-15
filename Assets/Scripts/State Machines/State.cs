using System;

[Serializable]
public abstract class State<T>
{
    protected T m_controller;

    public State(T context)
    {
        m_controller = context;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public abstract void HandleStateTransition();
}
