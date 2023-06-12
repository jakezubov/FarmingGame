using UnityEngine;

public class Skill : MonoBehaviour
{   
    public ChangeSlider _experienceBar;
    public ChangeText _levelText, _skillPointsText;
    public TraitHandler _traits;

    private int _currentExp, _level, _skillPoints;
    private readonly int _requiredExpBase = 100;

    public void LoadSkill(int level, int sp, int exp)
    {
        // used for when loading game
        _level = level;
        _levelText.SetText(level.ToString());
        SetSkillPoints(sp);

        SetExpBarMax(_requiredExpBase * level);
        SetCurrentExp(exp);
    }

    public (int, int, int) SaveSkill()
    {
        return (_level, _skillPoints, _currentExp);
    }

    public void AddToCurrentExp(int experience)
    {
        _currentExp += experience;
        _experienceBar.SetValue(_currentExp);

        if (_currentExp >= _requiredExpBase * _level)
        {
            int leftoverExp = _currentExp - (_requiredExpBase * _level);
            LevelUp();
            SetExpBarMax((_requiredExpBase * _level));
            SetCurrentExp(leftoverExp);
        }
    }

    public int GetLevel()
    {
        return _level;
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

    private void SetCurrentExp(int exp)
    {
        _currentExp = exp;
        _experienceBar.SetValue(_currentExp);
    }   

    private void SetExpBarMax(int value)
    {
        _experienceBar.SetMaxValue(value);
    }

    private void LevelUp()
    {
        _level++;
        _levelText.SetText(_level.ToString());
        _skillPoints++;
        _skillPointsText.SetText(_skillPoints.ToString());
        _traits.ChangeLockedText();
        _traits.UnlockTraits();
    }
}
