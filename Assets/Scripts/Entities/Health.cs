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
    public bool isPermaInvincible = false;

    [Header("Audio")]
    [SerializeField] private AudioElement m_hitSound;
    [SerializeField] private AudioElement m_deathSound;

    [field:Header("Events")]
    [field:SerializeField] public UnityEvent  OnHitEvent{ get; set; }
    [field:SerializeField] public UnityEvent OnDeathEvent { get; set; }

    public float Normalized => (m_maxHP > 0) ? (float)m_currentHP / (float)m_maxHP : 0;

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
        m_animator = GetComponent<Animator>();
    }

    public virtual void Hit(int damage)
    {
        if (IsDead) { return; }

        if (!isPermaInvincible)
        {
            CurrentHP -= damage;
        }
        OnHitEvent?.Invoke();
            
        m_animator.SetTrigger(_onHitAnimHash);

        PlayOneShotHit();

        OnDeath();
        
    }

    public virtual void OnInstantDeath()
    {
        if (IsDead) { return; }

        CurrentHP -= CurrentHP;
        OnDeath();
    }

    public virtual void OnDeath()
    {
        if (!IsDead) { return; }
        OnDeathEvent?.Invoke();
        m_animator.SetTrigger(_onDeathAnimHash);
        PlayOneShotDeath();
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

    private void PlayOneShotHit() => m_hitSound.PlayOneShot(m_hitSound.GetRandomClip());
    private void PlayOneShotDeath() => m_deathSound.PlayOneShot(m_deathSound.GetRandomClip());
}
