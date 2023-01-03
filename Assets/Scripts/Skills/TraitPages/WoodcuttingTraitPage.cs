using UnityEngine;

public class WoodcuttingTraitPage : MonoBehaviour
{
    public Trait _axeEfficiency, _leafClearing, _greaterChopper, _treeFertiliser, _lumberjack, _woodFarmer;

    private Skill _skill;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = PlayerManager._instance._woodcutting;
        _axeEfficiency.SetLevelUpStatus(true);
    }

    public void TraitLevelUp(Trait trait)
    {
        int currentSkillPoints = _skill.GetSkillPoints();

        if (trait.CheckLevelUpStatus() && currentSkillPoints > 0 && trait.GetLevel() < _maxLevel)
        {
            trait.IncreaseLevel();
            currentSkillPoints--;
            _skill.SetSkillPoints(currentSkillPoints);

            PerformTraitChange(trait);
            UnlockTraits();
        }       
    }

    private void PerformTraitChange(Trait trait)
    {
        if (trait == _axeEfficiency) { }
        else if (trait == _leafClearing) { }
        else if (trait == _greaterChopper) { }
        else if (trait == _treeFertiliser) { }
        else if (trait == _lumberjack) { }
        else if (trait == _woodFarmer) { }
    }

    private void UnlockTraits()
    {
        if (_axeEfficiency.GetLevel() >= _minLevel)
        {
            _leafClearing.MakeButtonAvaliable();
            _leafClearing.SetLevelUpStatus(true);
            _greaterChopper.MakeButtonAvaliable();
            _greaterChopper.SetLevelUpStatus(true);
        }
        if (_leafClearing.GetLevel() >= _minLevel)
        {
            _treeFertiliser.MakeButtonAvaliable();
            _treeFertiliser.SetLevelUpStatus(true);
            _lumberjack.MakeButtonAvaliable();
            _lumberjack.SetLevelUpStatus(true);
        }
        if (_greaterChopper.GetLevel() >= _minLevel)
        {
            _lumberjack.MakeButtonAvaliable();
            _lumberjack.SetLevelUpStatus(true);
            _woodFarmer.MakeButtonAvaliable();
            _woodFarmer.SetLevelUpStatus(true);
        }
    }
}
