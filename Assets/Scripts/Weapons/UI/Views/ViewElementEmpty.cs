using System;

public class ViewElementEmpty : ViewElement
{
    public override void OnShow(Action callback = null)
    {
        callback?.Invoke();
    }

    public override void OnHide(Action callback = null)
    {
        callback?.Invoke();
    }

    public override void OnHideQuick(Action callback = null)
    {
        callback?.Invoke();
    }
}
