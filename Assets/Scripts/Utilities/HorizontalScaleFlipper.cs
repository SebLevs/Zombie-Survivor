using UnityEngine;

public class HorizontalScaleFlipper : MonoBehaviour, IUpdateListener
{
    [field: Header("Target used for evaluating the angle")]
    [SerializeField] private bool isTargetPlayer = true;
    [field:SerializeField] public Transform Target { get; private set; }

    [Header("Mirror angle")]
    [SerializeField] private bool isMirror = false;
    private int _getMirrorValue => (isMirror) ? -1 : 1;

    private void Start()
    {
        if (isTargetPlayer)
        {
            SetTargetAsPlayer();
        }
    }

    public void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
    }

    public void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public void OnUpdate()
    {
        float angle = MathAngleUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, this.transform);
        MathAngleUtilities.FlipLocalScale2D(transform, angle * _getMirrorValue);
    }

    public void SetTargetAsPlayer()
    {
        Target = Entity_Player.Instance.transform;
    }

    public void SetTargetAs(Transform target)
    {
        if (target == null) { return; }
        Target = target;
    }
}
