using UnityEngine;

public class EXAMPLE_UpdatableAndFixedUpdatable : MonoBehaviour, IFixedUpdateListener, IFrameUpdateListener
{
    public void OnDisable()
    {
        UpdateManager.Instance.UnSubscribeFromFixedUpdate(this);
        UpdateManager.Instance.UnSubscribeFromUpdate(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToFixedUpdate(this);
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public void OnFixedUpdate()
    {
        Debug.Log($"OnFixedUpdate() of IFixedUpdateListener was called from {this.name}");
    }

    public void OnUpdate()
    {
        Debug.Log($"OnUpdate() of IFrameUpdateListener was called from {this.name}");
    }
}
