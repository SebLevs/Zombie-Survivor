using UnityEngine;

public class BoomerangBehavior : BaseProjectile, IPoolable
{
    public Vector2 startLocation;
    public Vector2 targetLocation;
    private bool _comingBack = false;
    public AnimationCurve moveCurveGo;
    public AnimationCurve moveCurveBack;
    public float moveDurationGo = 2f;
    public float moveDurationBack = 1f;
    private float _moveStopWatch = 0;
    public bool isShot = false;
    private float _rotationZ;
    [SerializeField] private float _rotationSpeed = 720f;
    
    [Header("Audio")]
    [SerializeField] private AudioElement boomSound;

    protected override void OnStart()
    {
    }

    protected override void OnAwake()
    {
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    protected override void OnUpdate()
    {
        if (isShot)
        {
            _moveStopWatch += Time.deltaTime;
            _rotationZ += Time.deltaTime * _rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, _rotationZ);

            if (_moveStopWatch >= moveDurationGo && !_comingBack)
            {
                _comingBack = true;
                _moveStopWatch = 0;
                transform.position = targetLocation;
            }
            if (_moveStopWatch >= moveDurationBack && _comingBack)
            {
                _comingBack = false;
                _moveStopWatch = 0;
                isShot = false;
                WeaponManager.Instance.boomPool.ReturnToAvailable(this);

            }
            if (!_comingBack)
            {
                transform.position = Vector2.Lerp(startLocation, targetLocation, moveCurveGo.Evaluate(_moveStopWatch / moveDurationGo));
            }
            else
            {
                transform.position = Vector2.Lerp(targetLocation, Entity_Player.Instance.transform.position, moveCurveBack.Evaluate(_moveStopWatch / moveDurationBack));
            }
        }
    }

    protected override void OnProjectileTriggerEnter(Collider2D collision)
    {
        base.OnProjectileTriggerEnter(collision);

        if (!EvaluateLayers(collision.gameObject.layer, TargetMask)) { return; }

        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.Hit(m_damage);
        }
    }


    public void OnGetFromAvailable()
    {
        _rotationZ = 0f;
        _rotationSpeed = 720f;
        startLocation = Entity_Player.Instance.transform.position;
        targetLocation = startLocation + (Player_Controller.Instance.normalizedLookDirection * Entity_Player.Instance.boomDistance);
    }

    public void OnReturnToAvailable()
    {
        boomSound.StopSound();
    }

    public void ShootBoom()
    {
        isShot = true;
        boomSound.PlaySound(boomSound.GetRandomClip());
        
    }
    
    //private void PlayOneShotHit() => boomSound.PlayOneShot(boomSound.GetRandomClip());

}
