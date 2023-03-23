using UnityEngine;

public class ViewPromoCode : ViewElement
{
    [field: SerializeField] public TMPLocalizablePair VisualCue { get; private set; }

    public void EnableAndSetVisualCue(KeyColorPair pair)
    {
        VisualCue.gameObject.SetActive(true);
        VisualCue.SetPair(pair);
        VisualCue.LocalizeText();
    }

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
