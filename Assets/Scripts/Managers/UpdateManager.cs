using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Centralisation of Updates and Fixed Updates<br/>
/// Reduces greatly the interop calls (a call from the C/C++ side to the managed C# side of Unity)<br/><br/>
/// https://resources.unity.com/games/performance-optimization-e-book-console-pc Page 20-21
/// </summary>
public class UpdateManager : Manager<UpdateManager>
{
    private HashSet<IUpdateListener> m_frameUpdateListeners;
    private HashSet<IFixedUpdateListener> m_fixedUpdateListeners;
    private HashSet<ILateUpdateListener> m_lateUpdateListeners;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_frameUpdateListeners = new HashSet<IUpdateListener>();
        m_fixedUpdateListeners = new HashSet<IFixedUpdateListener>();
        m_lateUpdateListeners = new HashSet<ILateUpdateListener>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused) { return; }
        for (int i = 0; i < m_frameUpdateListeners.Count; i++)
        {
            m_frameUpdateListeners.ElementAt(i).OnUpdate();
        }
    }

    public void SubscribeToUpdate(IUpdateListener frameUpdateListener)
    {
        m_frameUpdateListeners.Add(frameUpdateListener);
    }

    public void UnSubscribeFromUpdate(IUpdateListener frameUpdateListener)
    {
        m_frameUpdateListeners.Remove(frameUpdateListener);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused) { return; }
        foreach (IFixedUpdateListener fixedUpdateListener in m_fixedUpdateListeners)
        {
            fixedUpdateListener.OnFixedUpdate();
        }
    }

    public void SubscribeToFixedUpdate(IFixedUpdateListener fixedUpdateListener)
    {
        m_fixedUpdateListeners.Add(fixedUpdateListener);
    }

    public void UnSubscribeFromFixedUpdate(IFixedUpdateListener fixedUpdateListener)
    {
        m_fixedUpdateListeners.Remove(fixedUpdateListener);
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.IsPaused) { return; }
        foreach (ILateUpdateListener lateUpdateListener in m_lateUpdateListeners)
        {
            lateUpdateListener.OnLateUpdate();
        }
    }

    public void SubscribeToLateUpdate(ILateUpdateListener lateUpdateListener)
    {
        m_lateUpdateListeners.Add(lateUpdateListener);
    }

    public void UnSubscribeFromLateUpdate(ILateUpdateListener lateUpdateListener)
    {
        m_lateUpdateListeners.Remove(lateUpdateListener);
    }
}
