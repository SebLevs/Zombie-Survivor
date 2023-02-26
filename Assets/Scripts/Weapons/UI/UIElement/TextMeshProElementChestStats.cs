using UnityEngine;

// TODO: Refactor into a poolable item into a vertical layout
public class TextMeshProElementChestStats : TextMeshProElement, IPauseListener, IFrameUpdateListener
{
    [SerializeField] private float _disappearAftertime = 2.5f;
    [SerializeField] SequentialTimer _timer;
    protected override void OnAwake()
    {
        base.OnAwake();
        _timer = new SequentialTimer(_disappearAftertime, HideChestBonus);
    }

    public void OnDisable()
    {
        GameManager.Instance.UnSubscribeFromPauseGame(this);
        UpdateManager.Instance.UnSubscribeFromUpdate(this);
    }

    public void OnEnable()
    {
        GameManager.Instance.SubscribeToPauseGame(this);
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    public void OnPauseGame()
    {
        _timer.PauseTimer();
    }

    public void OnResumeGame()
    {
        _timer.StartTimer();
    }

    public void OnUpdate()
    {
        _timer.OnUpdateTime();
    }

    public void PrintChestBonus(string message) 
    {
        _timer.Reset();
        _timer.StartTimer();
        Element.text = message;
    }

    public void HideChestBonus()
    {
        Element.text = "";
    }
}
