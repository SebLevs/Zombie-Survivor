using UnityEngine;

public class ViewElementOptions : ViewElementButton
{
    [Header("Audio")]
    [SerializeField] private VolumeSlider[] m_volumeSliders;

    public void SetVolumeSlidersAsPlayerPrefs()
    {
        for (int i = 0; i < m_volumeSliders.Length; i++)
        {
            m_volumeSliders[i].SetVolumeAsLastRegisteredVolume();
        }
    }
}
