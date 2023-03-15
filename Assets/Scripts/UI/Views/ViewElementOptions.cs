using UnityEngine;

public class ViewElementOptions : ViewElementButton
{
    [Header("Audio")]
    [SerializeField] private VolumeSlider[] m_volumeSliders;

    [Header("Language")]
    [SerializeField] private DropDownLanguageSelection m_dropDownLanguageSelection;

    public void LoadVolumesFromPlayerPref()
    {
        for (int i = 0; i < m_volumeSliders.Length; i++)
        {
            m_volumeSliders[i].LoadFromPlayerPref();
        }
    }
}
