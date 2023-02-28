using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    [Header("Target layer mask as int")]
    [SerializeField] protected string[] ignoreTags;
    [SerializeField] [Min(0)] protected int[] targetMasks;

    protected Rigidbody2D m_rigidBody;
    protected Collider2D m_collider;

    protected virtual void OnAwake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
    }

    protected virtual void OnStart() { }

    protected virtual void OnEntityCollisionEnter(Collision2D collision) { }
    protected virtual void OnEntityCollisionExit(Collision2D collision) { }
    protected virtual void OnEntityTriggerEnter(Collider2D collision) { }
    protected virtual void OnEntityTriggerStay(Collider2D collision) { }
    protected virtual void OnEntityTriggerExit(Collider2D collision) { }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnEntityCollisionEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnEntityCollisionExit(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnEntityTriggerEnter(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnEntityTriggerStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPaused) { return; }
        OnEntityTriggerExit(collision);
    }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    protected bool IsValidForInteract(int otherLayer, string otherTag)
    {
        return IsOtherLayerAlsoTargetLayer(otherLayer) && !IsOtherTagIgnoreTag(otherTag);
    }

    private bool IsOtherLayerAlsoTargetLayer(int otherLayer)
    {
        foreach (int layer in targetMasks)
        {
            if (otherLayer == layer) { return true; }
        }
        return false;
    }

    private bool IsOtherTagIgnoreTag(string otherTag)
    {
        string tagLower = otherTag.ToLower();
        foreach (string tag in ignoreTags)
        {
            if (tagLower == tag.ToLower()) { return true; }
        }
        return false;
    }
}
