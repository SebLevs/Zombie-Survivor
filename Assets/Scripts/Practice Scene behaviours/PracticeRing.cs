using UnityEngine;

public class PracticeRing : MonoBehaviour
{
    [SerializeField] private WaveController waveController;

    private void Start()
    {
        UIManager.Instance.ViewWaveStats.OnHide();
        waveController.enabled = false;
    }

    public void StartPractice()
    {
        UIManager.Instance.ViewWaveStats.OnShow();
        waveController.enabled = true;
    }

    public void Cleanup()
    {
        waveController.enabled = false;
        EnemyManager.Instance.KillAllCurrentlyActiveEnemies();
        UIManager.Instance.ViewWaveStats.OnHide();
    }
}
