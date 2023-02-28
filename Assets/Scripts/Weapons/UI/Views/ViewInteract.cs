using UnityEngine;

public class ViewInteract : ViewElement
{
    private Transform currentInvoker;

    public void Init(Transform transform, Vector2 position)
    {
        currentInvoker = transform;
        SetPosition(position);
    }

    private void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void Deactivate(Transform invoker)
    {
        if (currentInvoker == invoker)
        {
            currentInvoker = null;
            OnHide();
        }
    }
}
