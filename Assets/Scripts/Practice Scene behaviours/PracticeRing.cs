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
        Collider2D[] collisions =  new Collider2D[100];
        Physics2D.OverlapBoxNonAlloc(transform.position, new Vector2(100, 100), 0, collisions);

        foreach (var collision in collisions)
        {
            if (!collision) { continue; }
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.ReturnToPool();
            }
        }

        UIManager.Instance.ViewWaveStats.OnHide();
    }
}
