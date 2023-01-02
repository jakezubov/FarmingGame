using UnityEngine;

public class MiningTraitPage : MonoBehaviour
{
    public AllSkills _allSkills;
    public Trait _pickaxeEfficiency, _mapMaker, _oreMiner, _engineer, _demolitionist, _archaeologist;

    private Skill _skill;
    private int _currentSkillPoints;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = _allSkills._mining;
        _pickaxeEfficiency.SetLevelUpStatus(true);
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
        if (trait == _pickaxeEfficiency) { }
        else if (trait == _mapMaker) { }
        else if (trait == _oreMiner) { }
        else if (trait == _engineer) { }
        else if (trait == _demolitionist) { }
        else if (trait == _archaeologist) { }
    }

    private void UnlockTraits()
    {
        if (_pickaxeEfficiency.GetLevel() >= _minLevel)
        {
            _mapMaker.MakeButtonAvaliable();
            _mapMaker.SetLevelUpStatus(true);
            _oreMiner.MakeButtonAvaliable();
            _oreMiner.SetLevelUpStatus(true);
        }
        if (_mapMaker.GetLevel() >= _minLevel)
        {
            _engineer.MakeButtonAvaliable();
            _engineer.SetLevelUpStatus(true);
            _demolitionist.MakeButtonAvaliable();
            _demolitionist.SetLevelUpStatus(true);
        }
        if (_oreMiner.GetLevel() >= _minLevel)
        {
            _demolitionist.MakeButtonAvaliable();
            _demolitionist.SetLevelUpStatus(true);
            _archaeologist.MakeButtonAvaliable();
            _archaeologist.SetLevelUpStatus(true);
        }
    }
}
