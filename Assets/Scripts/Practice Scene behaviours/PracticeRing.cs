using UnityEngine;

public class PracticeRing : MonoBehaviour
{
    [SerializeField] private WaveController waveController;

    private void Start()
    {
        UIManager.Instance.ViewWaveStats.OnHide();
        waveController.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        StartPractice();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        Cleanup();
    }

    private void StartPractice()
    {
        UIManager.Instance.ViewWaveStats.OnShow();
        waveController.enabled = true;
    }

    private void Cleanup()
    {
        waveController.enabled = false;
        EnemyManager.Instance.KillAllCurrentlyActiveEnemies();
        UIManager.Instance.ViewWaveStats.OnHide();
    }
}
