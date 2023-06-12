using UnityEngine;
using UnityEngine.UI;

public class ChangeSlider : MonoBehaviour
{
    public Slider _slider;

    public void SetMaxValue(int value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }

    public void SetValue(int value)
    {
        _slider.value = value;
    }

    public void LowerValue(int value)
    {
        _slider.value -= value;
    }

    public int GetValue()
    {
        return Mathf.RoundToInt(_slider.value);
    }
}
