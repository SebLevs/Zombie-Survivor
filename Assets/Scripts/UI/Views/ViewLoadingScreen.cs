using UnityEngine;

public class ViewLoadingScreen : ViewElement
{
    [field:Header("_slider")]
    [field: SerializeField] public ViewElementSlider ViewSlider { get; private set; }


    [Header("Animation")]
    [SerializeField] private Animator enemyAnimator;
    private int walkHash;
    private int attackHash;

    protected override void OnAwake()
    {
        base.OnAwake();
        walkHash = Animator.StringToHash("walk");
        attackHash = Animator.StringToHash("attack");
    }

    protected override void OnStart()
    {
        base.OnStart();
        Init();
    }

    public void Init()
    {
        ViewSlider.SetsliderValue(0f);
    }

    public void AnimateOnReachedEndValue()
    {
        enemyAnimator.SetTrigger(attackHash);
    }
}
