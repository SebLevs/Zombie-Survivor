/// <summary>
/// Must subscribe to the UpdateManager.cs OnEnable()<br/>
/// Must unsubscribe to the UpdateManager.cs OnDisable()
/// </summary>
public interface ILateUpdateListener
{
    public void OnLateUpdate();

    /// <summary>
    /// Used to subscribe to the UpdateManager.cs
    /// </summary>
    public void OnEnable();

    /// <summary>
    /// Used to unsubscribe from the UpdateManager.cs
    /// </summary>
    public void OnDisable();
}
