using UnityEngine;

public class ArrowDeactivator : MonoBehaviour
{
    [SerializeField] private bool disableAfterUse = true;
    [SerializeField] private string[] actOnTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CheckTags(collision.tag)) { return; }

        Entity_Player.Instance.arrow.gameObject.SetActive(false);
        if (disableAfterUse) { gameObject.SetActive(false); }
    }

    private bool CheckTags(string tag)
    {
        foreach (string tagRef in actOnTags)
        {
            if (tagRef == tag) { return true; }
        }

        return false;
    }
}
