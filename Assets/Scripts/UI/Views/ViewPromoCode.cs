public class ViewPromoCode : ViewElement
{
    public void TryShowView()
    {
#if UNITY_EDITOR
        if (Entity_Player.Instance.UserDatas == null) { return; }
#endif
        if (Entity_Player.Instance.UserDatas.emailVerified)
        {
            OnShow();
        }
    }
}
