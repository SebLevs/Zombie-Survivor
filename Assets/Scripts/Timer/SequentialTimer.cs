using System;
using UnityEngine;

[Serializable]
public class SequentialTimer : SequentialTimeCalculator
{
    public SequentialTimer(float targetTime) : base(targetTime)
    {
        CurrentTime = targetTime;
    }

    public SequentialTimer(float targetTime, Action callback) : base(targetTime, callback)
    {
        CurrentTime = targetTime;
    }

    public override bool HasReachedTarget()
    {
        return CurrentTime <= 0.0f;
    }

    public override bool OnUpdateTime()
    {
        if (IsPaused) { return HasReachedTarget(); }

        CurrentTime -= Time.deltaTime;
        if (HasReachedTarget())
        {
            PauseTimer();
            if (m_callback != null) { m_callback(); }
            return true;
        }

        return false;
    }

    public override void Reset(bool isPaused = false)
    {
        IsPaused = isPaused;
        CurrentTime = TargetTime;
    }
}
