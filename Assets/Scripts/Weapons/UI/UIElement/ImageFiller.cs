using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller: MonoBehaviour
{
    protected Image m_fillingBar;

    private void Awake()
    {
        m_fillingBar = GetComponent<Image>();
    }

    /// <summary>
    /// Set the filling of the bar based on a normalised (0 to 1) value <br/>
    /// Value will be clamped automatically at 1
    /// </summary>
    public virtual void SetFilling(float fillingNormalised)
    {
        m_fillingBar.fillAmount = (fillingNormalised > 1.0f) ? 1.0f : fillingNormalised;
    }

    public virtual void SetFilling(Health health)
    {
        m_fillingBar.fillAmount = (health.Normalized > 1.0f) ? 1.0f : health.Normalized;
    }

    public void UnfillCompletely()
    {
        m_fillingBar.fillAmount = 0;
    }

    public void FillUpCompletely()
    {
        m_fillingBar.fillAmount = 1;
    }
}
