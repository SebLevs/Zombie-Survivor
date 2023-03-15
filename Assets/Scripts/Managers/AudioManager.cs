using UnityEngine;


public class AudioManager : Manager<AudioManager>
{
    [SerializeField] private AudioMixerParameter _volumeMaster;

    // TODO: if colliderActiveTime to refactor - Transfer this scene specific audio logic into SceneController.cs for each specific scene
    private AudioSource _audioSource;
    [field: SerializeField] public AudioClip AmbianceClip { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        DelegateSetVolumeSlidersAsPlayerPrefs();
    }

    private void DelegateSetVolumeSlidersAsPlayerPrefs()
    {
        UIManager uiManager = UIManager.Instance;
        uiManager.ViewOptionMenu.gameObject.SetActive(true);
        uiManager.ViewOptionMenu.LoadVolumesFromPlayerPref();
        uiManager.ViewOptionMenu.gameObject.SetActive(false);
    }

    public void MuteGame()
    {
        _volumeMaster.SetParameter(-80);
    }

    public void UnMuteGame()
    {
        _volumeMaster.SetParameter(0);
    }

    public void PlayLoopingClip(AudioClip _clip)
    {
        _audioSource.clip = AmbianceClip;
        _audioSource.Play();
    }

    public void StopPlayingLoopingClip()
    {
        _audioSource.Stop();
    }
}
