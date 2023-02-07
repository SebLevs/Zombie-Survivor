using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerParameter _volumeParameter;
    private Slider m_slider;

    private float _lastRegisteredVolume = 1f;

    private void Awake()
    {
        m_slider = GetComponent<Slider>();
    }

    public void SetVolume(float volume)
    {
        volume = GetLog(volume);
        _volumeParameter.SetParameter(volume);
    }

    public void SetVolumeAsLastRegisteredVolume()
    {
        float volume = GetLog(_lastRegisteredVolume);
        _volumeParameter.SetParameter(volume);
    }

    public void SaveLastRegisteredVolumeLocally()
    {
        _lastRegisteredVolume = m_slider.value;
    }

    /// <summary>
    /// log10(0.0001) * 20 = -80 = no sound <br/>
    /// log10(1) * 20 = 20 = max audio
    /// </summary>
    private float GetLog(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }
}
