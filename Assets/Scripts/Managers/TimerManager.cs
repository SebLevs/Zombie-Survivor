using System;
using System.Collections.Generic;
using System.Linq;

public class TimerManager : Manager<TimerManager>
{
    private List<SequentialStopwatch> m_stopwatches; // ->
    private List<SequentialTimer> m_timers; // <-
    public int StopwatchesCount => m_stopwatches.Count;
    public int TimersCount => m_timers.Count;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_stopwatches = new List<SequentialStopwatch>();
        m_timers = new List<SequentialTimer>();
    }

    private void Update()
    {
        // if (GameManager.Instance.IsGamePaused) { return; }
        if (m_stopwatches.Any())
        {
            for (int i = 0; i < m_stopwatches.Count; i++)
            {
                if (m_stopwatches[i].OnUpdateTime())
                {
                    m_stopwatches.RemoveAt(i);
                }
            }
        }


        if (m_timers.Any())
        {
            for (int i = 0; i < m_timers.Count; i++)
            {
                if (m_timers[i].OnUpdateTime())
                {
                    m_timers.RemoveAt(i);
                }
            }
        }
    }

    public SequentialStopwatch AddSequentialStopwatch(float desiredTime, Action callback)
    {
        SequentialStopwatch timer = new SequentialStopwatch(desiredTime, callback);
        m_stopwatches.Add(timer);
        return timer;
    }

    public SequentialStopwatch AddSequentialStopwatch(float desiredTime)
    {
        SequentialStopwatch timer = new SequentialStopwatch(desiredTime);
        m_stopwatches.Add(timer);
        return timer;
    }

    public SequentialTimer AddSequentialTimer(float desiredTime, Action callback)
    {
        SequentialTimer timer = new SequentialTimer(desiredTime, callback);
        m_timers.Add(timer);
        return timer;
    }

    public SequentialTimer AddSequentialTimer(float desiredTime)
    {
        SequentialTimer timer = new SequentialTimer(desiredTime);
        m_timers.Add(timer);
        return timer;
    }

    public void RemoveSequentialStopWatch(SequentialStopwatch callbackTimer)
    {
        m_stopwatches.Remove(callbackTimer);
    }

    public void RemoveSequentialTimer(SequentialTimer callbackTimer)
    {
        m_timers.Remove(callbackTimer);
    }
}
