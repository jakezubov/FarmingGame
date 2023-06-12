using UnityEngine;

public class Stat : MonoBehaviour
{
    public ChangeSlider _slider;
    public ChangeText _textCS; // Character sheet text

    private float _maxValue, _currentValue;

    public void LoadStat(float max, float current)
    {
        _maxValue = max;
        _currentValue = current;
        UpdateStat();
    }

    public (float, float) SaveStat()
    {
        return (_maxValue, _currentValue);
    }

    public void LowerStatAmount(float value)
    {
        _currentValue -= value;
        UpdateStat();
    }

    public void ReplenishStatAmount(float value)
    {
        _currentValue += value;
        if (_currentValue > _maxValue) { _currentValue = _maxValue; }
        UpdateStat();
    }

    public void AddToMaxStatAmount(float value)
    {
        _maxValue += value;
        UpdateStat();
    }

    public void UpdateStat()
    {
        // makes any changes to stat appear in the spellbook and sliders
        _textCS.SetText($"{gameObject.name}: {Mathf.RoundToInt(_currentValue)} / {Mathf.RoundToInt(_maxValue)}");
        _slider.SetMaxValue(Mathf.RoundToInt(_maxValue));
        _slider.SetValue(Mathf.RoundToInt(_currentValue));
    }

    public float GetCurrentValue()
    {
        return _currentValue;
    }
}


