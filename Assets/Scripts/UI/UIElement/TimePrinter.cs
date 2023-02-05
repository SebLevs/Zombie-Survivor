using System;
using TMPro;
using UnityEngine;

public enum TimeFormat
{
    HMSF,
    HMS,
    HM,
    H,
    S
}

public class TimePrinter : MonoBehaviour
{
    protected TextMeshProUGUI m_textMeshProUGUI;

    private void Awake()
    {
        m_textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetTime(float time, TimeFormat format)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        switch (format)
        {
            case TimeFormat.HMSF: m_textMeshProUGUI.text = timeSpan.ToString("hh':'mm':'ss':'ff"); break;
            case TimeFormat.HMS: m_textMeshProUGUI.text = timeSpan.ToString("hh':'mm':'ss"); break;
            case TimeFormat.HM: m_textMeshProUGUI.text = timeSpan.ToString("hh':'mm"); break;
            case TimeFormat.H: m_textMeshProUGUI.text = timeSpan.ToString("hh"); break;
            case TimeFormat.S: m_textMeshProUGUI.text = timeSpan.ToString("ss"); break;
        }
    }
}
