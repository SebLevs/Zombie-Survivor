using UnityEngine;

public class ViewElementOptions : ViewElementButton
{
    [field: SerializeField] public TestButtonTextUpdater TextButtonText { get; private set; }

    [Header("Audio")]
    [SerializeField] private VolumeSlider[] m_volumeSliders;

    [Header("Language")]
    [SerializeField] private DropDownLanguageSelection m_dropDownLanguageSelection;

    public void LoadVolumesFromPlayerPref()
    {
        m_volumeSliders[0].transform.parent.gameObject.SetActive(true);
        for (int i = 0; i < m_volumeSliders.Length; i++)
        {
            m_volumeSliders[i].LoadFromPlayerPref();
        }
        m_volumeSliders[0].transform.parent.gameObject.SetActive(false);
    }
}
