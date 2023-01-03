using UnityEngine;

public class TraitPages : MonoBehaviour
{
    public Trait _trait1, _trait2, _trait3, _trait4, _trait5, _trait6;
    public Skill _skill;

    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _trait1.SetLevelUpStatus(true);
        ChangeLockedText(_trait1);
        ChangeLockedText(_trait2);
        ChangeLockedText(_trait3);
        SetAllModifiers();
    }

    public virtual void PerformTraitChange(Trait trait)
    {

    }

    public virtual void ImprovementsPerLevel(Trait trait)
    {

    }

    public virtual void SetAllModifiers()
    {

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
            ChangeLockedText(trait);
            ImprovementsPerLevel(trait);
            UnlockTraits();
        }
    }  

    private void ChangeLockedText(Trait trait)
    {
        int trait5LevelsToUnlock;
        if (_trait2.GetLevel() > _trait3.GetLevel()) { trait5LevelsToUnlock = _trait2.GetLevelsToUnlock(); }
        else { trait5LevelsToUnlock = _trait3.GetLevelsToUnlock(); }

        if (trait == _trait1)
        {
            _trait2.GetComponent<TooltipTrigger>().SetColouredText($"{_trait2.name} will unlock in {_trait1.GetLevelsToUnlock()} levels", "Red");
            _trait3.GetComponent<TooltipTrigger>().SetColouredText($"{_trait3.name} will unlock in {_trait1.GetLevelsToUnlock()} levels", "Red");
        }
        else if (trait == _trait2)
        {
            _trait4.GetComponent<TooltipTrigger>().SetColouredText($"{_trait4.name} will unlock in {_trait2.GetLevelsToUnlock()} levels", "Red");
            _trait5.GetComponent<TooltipTrigger>().SetColouredText($"{_trait5.name} will unlock in {trait5LevelsToUnlock} levels", "Red");
        }
        else if (trait == _trait3)
        {
            _trait5.GetComponent<TooltipTrigger>().SetColouredText($"{_trait5.name} will unlock in {trait5LevelsToUnlock} levels", "Red");
            _trait6.GetComponent<TooltipTrigger>().SetColouredText($"{_trait6.name} will unlock in {_trait3.GetLevelsToUnlock()} levels", "Red");
        }
    }

    private void UnlockTraits()
    {
        if (_trait1.GetLevel() >= _minLevel)
        {
            _trait2.MakeButtonAvaliable();
            _trait2.SetLevelUpStatus(true);
            _trait3.MakeButtonAvaliable();
            _trait3.SetLevelUpStatus(true);
        }
        if (_trait2.GetLevel() >= _minLevel)
        {
            _trait4.MakeButtonAvaliable();
            _trait4.SetLevelUpStatus(true);
            _trait5.MakeButtonAvaliable();
            _trait5.SetLevelUpStatus(true);
        }
        if (_trait3.GetLevel() >= _minLevel)
        {
            _trait5.MakeButtonAvaliable();
            _trait5.SetLevelUpStatus(true);
            _trait6.MakeButtonAvaliable();
            _trait6.SetLevelUpStatus(true);
        }
    }
}
