using UnityEngine;

public class Stats : MonoBehaviour
{
    public ChangeSlider _slider;
    public ChangeText _textCS; // Character sheet text

    private int _maxAmount = 100;
    private int _currentAmount;

    public Stats()
    {
        
    }

    private void Start()
    {
        _currentAmount = _maxAmount;
        _slider.SetMaxValue(_maxAmount);
        UpdateCStext();
    }

    public void LowerCurrentStatAmount(int value)
    {
        _currentAmount -= value;
        _slider.SetValue(_currentAmount);
        UpdateCStext();
    }

    public void ReplenishCurrentStatAmount(int value)
    {
        _currentAmount += value;
        _slider.SetValue(_currentAmount);
        UpdateCStext();
    }

    public int GetCurrentStatAmount()
    {
        return _currentAmount;
    }

    public void AddToMaxStatAmount(int value)
    {
        _maxAmount += value;
        _slider.SetMaxValue(_maxAmount);
        _slider.SetValue(_currentAmount);
        UpdateCStext();
    }

    public void UpdateCStext()
    {
        _textCS.SetText($"{gameObject.name}: {_currentAmount} / {_maxAmount}");
    }
}
