using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptables/AudioMixer/Parameter", fileName = "Exposed parameter Name")]
public class AudioMixerParameter : ScriptableObject
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetParameter(float value)
    {
        _audioMixer.SetFloat(name, value);
    }
}