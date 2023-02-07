using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller: MonoBehaviour
{
    [Header("Filling bar")]
    [SerializeField] protected Image m_fillingBar;

    private void Awake()
    {
        m_fillingBar = GetComponent<Image>();
    }

    /// <summary>
    /// Set the filling of the bar based on a normalised (0 to 1) value <br/>
    /// Value will be clamped automatically at 1
    /// </summary>
    public virtual void SetFilling(float _fillingNormalised)
    {
        m_fillingBar.fillAmount = (_fillingNormalised > 1.0f) ? 1.0f : _fillingNormalised;
    }

    public void ResetFilling()
    {
        m_fillingBar.fillAmount = 0;
    }
}
