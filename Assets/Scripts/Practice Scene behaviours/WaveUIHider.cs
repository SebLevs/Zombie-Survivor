using UnityEngine;

public class WaveUIHider : MonoBehaviour
{
    private SequentialTimer _timer;

    private void Awake()
    {
        _timer = new SequentialTimer(3, () => UIManager.Instance.ViewWaveStats.OnHide());
    }

    private void Update()
    {
        _timer.OnUpdateTime();
    }
}
