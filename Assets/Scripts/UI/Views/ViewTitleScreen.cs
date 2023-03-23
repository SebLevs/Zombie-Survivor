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

    public void Logout()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ViewPromoCode.OnHide();
        uiManager.ViewController.SwitchViewSequential(uiManager.ViewLogin);
        Entity_Player.Instance.UserDatas = null;
    }
}
