public interface IPauseListener
{
    public void OnEnable();
    public void OnDisable();

    public void OnPauseGame();
    public void OnResumeGame();
}
