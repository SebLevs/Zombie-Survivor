using UnityEngine;
using UnityEngine.UI;

public class ViewTitleScreen : ViewElementButton
{
    [SerializeField] public Button quitButton { get; private set; }

    protected override void OnStart()
    {
        base.OnStart();
        OnWebGLCleanup();
    }

    public void OnWebGLCleanup()
    {
#if UNITY_WEBGL
        if (quitButton)
        {
            Destroy(quitButton.gameObject);
        }
#endif
    }
}
