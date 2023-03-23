using UnityEngine;

public class DeactivateGameObjectAfterTime : MonoBehaviour, IUpdateListener
{
    [SerializeField] private bool isAlwaysUpdatable;
    [SerializeField] private float _setInativeInTime;
    private SequentialTimer _timer;

    private void Awake()
    {
        _timer = new SequentialTimer(_setInativeInTime, () =>
        {
            gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        if (isAlwaysUpdatable)
        {
            OnUpdate();
        }
    }

    public void OnDisable()
    {
        if (!isAlwaysUpdatable)
        {
            UpdateManager.Instance?.UnSubscribeFromUpdate(this);
        }
    }

    public void OnEnable()
    {
        if (!isAlwaysUpdatable)
        {
            UpdateManager.Instance?.SubscribeToUpdate(this);
        }
        _timer.Reset();
    }

    public void OnUpdate()
    {
        _timer.OnUpdateTime();
    }
}
