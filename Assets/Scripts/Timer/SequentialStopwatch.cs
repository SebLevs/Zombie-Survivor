using System;
using UnityEngine;

[Serializable]
public class SequentialStopwatch : SequentialTimeCalculator
{
    public SequentialStopwatch(float targetTime) : base(targetTime)
    {
    }

    public SequentialStopwatch(float targetTime, Action callback) : base(targetTime, callback)
    {
    }

    public override bool HasReachedTarget()
    {
        return CurrentTime >= TargetTime;
    }

    public override bool OnUpdateTime()
    {
        if (IsPaused) { return HasReachedTarget(); }

        CurrentTime += Time.deltaTime;
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
        CurrentTime = 0;
    }
}
