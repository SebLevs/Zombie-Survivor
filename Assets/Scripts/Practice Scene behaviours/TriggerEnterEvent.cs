using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEnterEvent;
    [SerializeField] private string[] triggerTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsCollisionFromTag(collision.tag)) { return; }
        triggerEnterEvent.Invoke();
    }

    private bool IsCollisionFromTag(string collisionTag)
    {
        foreach (var tag in triggerTags)
        {
            if (tag == collisionTag)
            {
                return true;
            }
        }

        return false;
    }
}
