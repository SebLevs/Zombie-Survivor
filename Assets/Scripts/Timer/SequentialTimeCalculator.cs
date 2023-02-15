using UnityEngine;
using System;

[Serializable]
public abstract class SequentialTimeCalculator
{
    protected Action m_callback = () => { };
    public float TargetTime { get; protected set; }
    public float CurrentTime { get; protected set; }
    public bool IsPaused { get; protected set; }
    public float GetNormal => Mathf.Abs(CurrentTime / TargetTime);

    public SequentialTimeCalculator(float targetTime)
    {
        TargetTime = targetTime;

        IsPaused = false;
    }

    public SequentialTimeCalculator(float targetTime, Action callback)
    {
        TargetTime = targetTime;

        m_callback = callback;

        IsPaused = false;
    }

    public abstract bool HasReachedTarget();

    /// <summary>
    /// Will not update if IsPaused
    /// </summary>
    /// <returns>HasReachedEndOfPath</returns>
    public abstract bool OnUpdateTime();

    public void StartTimer()
    {
        IsPaused = false;
    }

    public void PauseTimer()
    {
        IsPaused = true;
    }

    public abstract void Reset(bool isPaused = false);

    public void SetNewTarget(float target, bool isPaused = false)
    {
        TargetTime = target;
        Reset(isPaused);
    }

    public void JumpToTime(float jumpTo)
    {
        CurrentTime = jumpTo;
    }
}
