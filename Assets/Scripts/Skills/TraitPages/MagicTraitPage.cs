using UnityEngine;

public class MagicTraitPage : MonoBehaviour
{
    public AllSkills _allSkills;
    public Trait _swiftness, _packMule, _manaOverload, _spellAffinity, _willpower, _ritualAffinity;
    public Stat _mana;

    private Skill _skill;
    private int _currentSkillPoints;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = _allSkills._magic;
        _swiftness.SetLevelUpStatus(true);
    }

    public void TraitLevelUp(Trait trait)
    {
        _currentSkillPoints = _skill.GetSkillPoints();
        if (trait.CheckLevelUpStatus() && _skill.GetSkillPoints() > 0 && trait.GetLevel() < _maxLevel)
        {
            trait.IncreaseLevel();
            _currentSkillPoints--;
            _skill.SetSkillPoints(_currentSkillPoints);

            PerformTraitChange(trait);
            UnlockTraits();
        }       
    }

    private void PerformTraitChange(Trait trait)
    {
        if (trait == _swiftness) { }
        else if (trait == _packMule) { }
        else if (trait == _manaOverload) { _mana.AddToMaxStatAmount(10); }
        else if (trait == _spellAffinity) { }
        else if (trait == _willpower) { }
        else if (trait == _ritualAffinity) { }
    }

    private void UnlockTraits()
    {
        if (_swiftness.GetLevel() >= _minLevel)
        {
            _packMule.MakeButtonAvaliable();
            _packMule.SetLevelUpStatus(true);
            _manaOverload.MakeButtonAvaliable();
            _manaOverload.SetLevelUpStatus(true);
        }
        if (_packMule.GetLevel() >= _minLevel)
        {
            _spellAffinity.MakeButtonAvaliable();
            _spellAffinity.SetLevelUpStatus(true);
            _willpower.MakeButtonAvaliable();
            _willpower.SetLevelUpStatus(true);
        }
        if (_manaOverload.GetLevel() >= _minLevel)
        {
            _willpower.MakeButtonAvaliable();
            _willpower.SetLevelUpStatus(true);
            _ritualAffinity.MakeButtonAvaliable();
            _ritualAffinity.SetLevelUpStatus(true);
        }
    }
}
