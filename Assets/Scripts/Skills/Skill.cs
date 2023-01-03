using UnityEngine;

public class Skill : MonoBehaviour
{   
    public ChangeSlider _experienceBar;
    public ChangeText _levelText, _skillPointsText;

    private int _currentExp, _level, _skillPoints;
    private readonly int _requiredExpBase = 100;

    public Skill()
    {
        _level = 1;       
        _currentExp = 0;
        _skillPoints = 0;       
    }

    private void Start()
    {
        _levelText.SetText(_level.ToString());
    }

    public void GainExp(int experience)
    {
        AddToCurrentExp(experience);
        if (_currentExp >= _requiredExpBase * _level)
        {
            int leftoverExperience = _currentExp - (_requiredExpBase * _level);
            LevelUp();
            SetExpBarMax(_requiredExpBase * GetLevel());
            SetCurrentExp(leftoverExperience);
        }
    }

    public int GetCurrentExp()
    {
        return _currentExp;
    }

    public void SetCurrentExp(int exp)
    {
        _currentExp = exp;
        _experienceBar.SetValue(_currentExp);
    }

    public void AddToCurrentExp(int exp)
    {
        _currentExp += exp;
        _experienceBar.SetValue(_currentExp);
    }

    public int GetLevel()
    {
        return _level;
    }

    public void LevelUp()
    {
        _level++;
        _levelText.SetText(_level.ToString());
        _skillPoints++;
        _skillPointsText.SetText(_skillPoints.ToString());
    }

    public int GetSkillPoints()
    {
        return _skillPoints;
    }

    public void SetSkillPoints(int value)
    {
        _skillPoints = value;
        _skillPointsText.SetText(_skillPoints.ToString());
    }

    public void SetExpBarMax(int value)
    {
        _experienceBar.SetMaxValue(value);
    }

    
}
