using UnityEngine;
using UnityEngine.UI;

public class ViewElementSlider : ViewElement
{
    [Header("Slider")]
    [SerializeField] private Slider slider;

    public float GetSliderValue() => slider.value;
    public void SetsliderValue(float value)
    {
        slider.value = value;
    }
}
