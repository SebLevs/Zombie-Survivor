using UnityEngine;

public class IndependantEnemyAnimator : MonoBehaviour, IUpdateListener, IPauseListener
{
    [SerializeField] private bool isSpriteFlippable = true;
    protected SpriteRenderer m_spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public virtual void OnUpdate()
    {
        float angle = MathAngleUtilities.GetSignedAngle2D(Entity_Player.Instance.transform, transform);
        int angleIndex = MathAngleUtilities.GetAngleAsIndex2D_Quad(angle);
        if (isSpriteFlippable) { FlipSpriteHorizontally(angleIndex); }
        _animator.SetFloat("angle", angleIndex);
    }

    public virtual void OnDisable()
    {
        if (UpdateManager.Instance)
        {
            UpdateManager.Instance.UnSubscribeFromUpdate(this);
        }
        if (GameManager.Instance)
        {
            GameManager.Instance.UnSubscribeFromPauseGame(this);
        }
    }

    public virtual void OnEnable()
    {
        UpdateManager.Instance.SubscribeToUpdate(this);
        GameManager.Instance.SubscribeToPauseGame(this);
    }

    public virtual void OnPauseGame()
    {
        _animator.speed = 0f;
    }

    public virtual void OnResumeGame()
    {
        _animator.speed = 1f;
    }

    /// <summary>
    /// Currently using a counterclockwise quardinal setup where index at right = 0 and up = 1
    /// </summary>
    public void FlipSpriteHorizontally(int angleIndex)
    {
        m_spriteRenderer.flipX = (angleIndex == 2) ? true : false;
    }
}
