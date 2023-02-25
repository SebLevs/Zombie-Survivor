using System;
using UnityEngine;

enum ExecuteAtDistanceType
{
    NULL,
    AT,
    NOTAT,
    BELLOW,
    BELLOWORAT,
    ABOVE,
    ABOVEORAT,
}

[CreateAssetMenu(menuName = "Scriptables/Enemy/Skill", fileName = "Enemy skill")]
public class EnemySkill : ScriptableObject
{
    [field: Header("Animation")]
    [field: SerializeField] public AnimationClip Animation { get; private set; }

    [field: Header("Reaction time: Used for attack, state transitions, etc.")]
    [field: Tooltip(
    "Human reaction time in seconds (average): \n" +
    "Visual stimulus: ~0.25\n" +
    "Audio stimulus: ~0.17\n" +
    "Touch stimulus: ~0.15\n")]
    [field: Min(0)][field: SerializeField] public float ReactionTime { get; private set; }

    [field: Header("Distance requirements")]
    [field: SerializeField] private ExecuteAtDistanceType _executeAtDistanceType;
    [field: Min(0)][field: SerializeField] public float ExecuteAtDistance { get; private set; }

    // TODO: Refactor, bad math
    [field: Header("Angle requirements")]
    [SerializeField] private bool _canExecuteFromAnyAngle = true;
    [Tooltip("0 degrees = transform.right")]
    [SerializeField][Range(-180, 180)] private float _startAngle;
    [SerializeField] [Range(0, 180)] private float _angleRange;

    public void SetAnimatorTrigger(Animator animator)
    {
        animator.SetTrigger(Animation.name);
    }

    public void SetAnimatorBool(Animator animator, bool value)
    {
        animator.SetBool(Animation.name, value);
    }

    public bool CanExecute(Transform target, Transform self)
    {
        return IsTargetAtAngle(target, self) && IsInRange(target, self);
    }

    public bool IsInRange(Transform target, Transform self)
    {
        bool canExecute = false;
        float distance = LinearAlgebraUtilities.GetDistance2D(target.position, self.position);

        switch (_executeAtDistanceType)
        {
            case ExecuteAtDistanceType.AT:
                {
                    if (distance == ExecuteAtDistance) { canExecute = true; }
                    break;
                }
            case ExecuteAtDistanceType.NOTAT:
                {
                    if (distance != ExecuteAtDistance) { canExecute = true; }
                    break;
                }
            case ExecuteAtDistanceType.BELLOW:
                {
                    if (distance < ExecuteAtDistance) { canExecute = true; }
                    break;
                }
            case ExecuteAtDistanceType.BELLOWORAT:
                {
                    if (distance <= ExecuteAtDistance) { canExecute = true; }
                    break;
                }
            case ExecuteAtDistanceType.ABOVE:
                {
                    if (distance > ExecuteAtDistance) { canExecute = true; }
                    break;
                }
            case ExecuteAtDistanceType.ABOVEORAT:
                {
                    if (distance >= ExecuteAtDistance) { canExecute = true; }
                    break;
                }
            default: break;
        }

        return canExecute;
    }

    /// <summary>
    /// 0 degress = self.transform.right
    /// </summary>
    public bool IsTargetAtAngle(Transform target, Transform self)
    {
        if (_canExecuteFromAnyAngle) { return true; }

        float angle = MathAngleUtilities.GetSignedAngle2D(target, self);

        bool canExecute = false;

        if (angle >= _startAngle - _angleRange && angle <= _startAngle + _angleRange)
        {
            canExecute = true;
        }

        return canExecute;
    }
}
