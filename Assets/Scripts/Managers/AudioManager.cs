using UnityEngine;


public class AudioManager : Manager<AudioManager>
{
    [SerializeField] private AudioMixerParameter _volumeMaster;

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    public void MuteGame()
    {
        _volumeMaster.SetParameter(-80);
    }

    public void UnMuteGame()
    {
        _volumeMaster.SetParameter(0);
    }
}
