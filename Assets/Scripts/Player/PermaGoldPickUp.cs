using UnityEngine;

public class PermaGoldPickUp : MonoBehaviour,IPoolable
{
    private const int Value = 3;
    private Collider2D _col;
    private SpriteRenderer _ren;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        _ren = GetComponent<SpriteRenderer>();
        _ren.enabled = false;
        _col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Entity_Player.Instance.permanentStats.GivePermaGold(Value);
            EnemyManager.Instance.PermaGold.ReturnToAvailable(this);
        }
    }

    public void OnGetFromAvailable()
    {
        _col.enabled = true;
        _ren.enabled = true;
    }

    public void OnReturnToAvailable()
    {
        _ren.enabled = false;
        _col.enabled = false;
    }
}
