using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator m_animator;
    private readonly int _onHitAnimHash = Animator.StringToHash("hit");
    private readonly int _onDeathAnimHash = Animator.StringToHash("death");

    [Header("Health")]
    [SerializeField] private int m_maxHP;

    [field:Header("Events")]
    [field:SerializeField] public UnityEvent OnHitEvent { get; private set; }
    [field:SerializeField] public UnityEvent OnDeathEvent { get; private set; }

     public int MaxHP
     {
        get { return m_maxHP; }
        private set
        {
            if (value < 0) { m_maxHP = 0; }
            else { m_maxHP = value; }
        }
     }

    private int m_currentHP;
    public int CurrentHP
    { 
        get { return m_currentHP; }
        set
        {
            if (value < 0) { m_currentHP = 0; }
            else if (value > MaxHP) { m_currentHP = MaxHP; }
            else { m_currentHP = value; }
        }
    }

    //[Header("Audio")]
    //[SerializeField] protected AudioController m_audioControllerHit;
    //[SerializeField] protected AudioController m_audioControllerDeath;

    //public AudioSource AudioSource { get; protected set; }

    public bool IsDead => CurrentHP <= 0;

    private void Awake() { Init(); }

    private void Init()
    {
        FullHeal();
    }

    public virtual void Hit(int damage)
    {
        if (!IsDead)
        {
            CurrentHP -= damage;

            OnHitEvent?.Invoke();
            // m_audioControllerHit.PlayOneShot();
            // TODO: play animation here
            // TODO: play sound here

            OnDeath();
        }
    }

    public virtual void OnInstantDeath()
    {
        CurrentHP -= CurrentHP;
        OnDeath();
    }

    public virtual void OnDeath()
    {
        if (IsDead)
        {
            OnDeathEvent?.Invoke();
            // TODO: play animation here
            // TODO: play sound here
        }
    }

    public void SetCurrentHP(int value)
    {
        CurrentHP = value;
    }
    public void SetMaxHP(int value)
    {
        MaxHP = value;
    }

    public void Heal(int value)
    {
        CurrentHP += value;
    }

    public void FullHeal()
    {
        CurrentHP = MaxHP;
    }
}