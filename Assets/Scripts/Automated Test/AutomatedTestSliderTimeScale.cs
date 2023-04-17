using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutomatedTestSliderTimeScale : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpTimeScale;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        Time.timeScale = 1;
        SetSliderValues();
    }

    public void SetSliderValues()
    {
        _slider.value = Time.timeScale > 0 ? Time.timeScale : _slider.minValue;
        tmpTimeScale.text = _slider.value.ToString();
    }

    public void OnValueChanged()
    {
        Time.timeScale = _slider.value;
        tmpTimeScale.text = _slider.value.ToString();
    }
}
