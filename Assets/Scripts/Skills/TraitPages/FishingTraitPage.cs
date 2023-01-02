using UnityEngine;

public class FishingTraitPage : MonoBehaviour
{
    public AllSkills _allSkills;
    public Trait _castMaster, _baitFinder, _highQualityLures, _noTimeToLose, _breedThemBig, _keenSight;

    private Skill _skill;
    private int _currentSkillPoints;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = _allSkills._fishing;
        _castMaster.SetLevelUpStatus(true);
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
        if (trait == _castMaster) { }
        else if (trait == _baitFinder) { }
        else if (trait == _highQualityLures) { }
        else if (trait == _noTimeToLose) { }
        else if (trait == _breedThemBig) { }
        else if (trait == _keenSight) { }
    }

    private void UnlockTraits()
    {
        if (_castMaster.GetLevel() >= _minLevel)
        {
            _baitFinder.MakeButtonAvaliable();
            _baitFinder.SetLevelUpStatus(true);
            _highQualityLures.MakeButtonAvaliable();
            _highQualityLures.SetLevelUpStatus(true);
        }
        if (_baitFinder.GetLevel() >= _minLevel)
        {
            _noTimeToLose.MakeButtonAvaliable();
            _noTimeToLose.SetLevelUpStatus(true);
            _breedThemBig.MakeButtonAvaliable();
            _breedThemBig.SetLevelUpStatus(true);
        }
        if (_highQualityLures.GetLevel() >= _minLevel)
        {
            _breedThemBig.MakeButtonAvaliable();
            _breedThemBig.SetLevelUpStatus(true);
            _keenSight.MakeButtonAvaliable();
            _keenSight.SetLevelUpStatus(true);
        }
    }
}
