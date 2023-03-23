public class ViewPromoCode : ViewElement
{
    public void TryShowView()
    {
        if (Entity_Player.Instance.UserDatas.emailVerified)
        {
            OnShow();
        }
    }
}
