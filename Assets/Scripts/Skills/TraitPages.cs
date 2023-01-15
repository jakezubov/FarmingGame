using UnityEngine;
using UnityEngine.EventSystems;

public class TraitPages : MonoBehaviour
{  
    public Trait _trait1, _trait2, _trait3, _trait4, _trait5, _trait6;
    public Skill _skill;

    private readonly int _minLevel = 3, _maxLevel = 10;
    private bool _trait1Unlocks = true, _trait2Unlocks = true, _trait3Unlocks = true;

    private void Start()
    {
        _trait1.UnlockTrait();
        ChangeLockedText(_trait1);
        ChangeLockedText(_trait2);
        ChangeLockedText(_trait3);
    }

    public virtual void PerformTraitChange(Trait trait)
    {
        Debug.Log("No assigned code");
    }

    public void TraitLevelUp()
    {
        Trait trait = EventSystem.current.currentSelectedGameObject.GetComponentInParent<Trait>();
        int currentSkillPoints = _skill.GetSkillPoints();

        if (trait.CheckLevelUpStatus() && currentSkillPoints > 0 && trait.GetLevel() < _maxLevel)
        {
            trait.IncreaseLevel();
            currentSkillPoints--;
            _skill.SetSkillPoints(currentSkillPoints);

            PerformTraitChange(trait);
            ChangeLockedText(trait);
            UnlockTraits();
        }
    }  

    private void ChangeLockedText(Trait trait)
    {
        int trait5LevelsToUnlock;
        if (_trait2.GetLevel() > _trait3.GetLevel()) { trait5LevelsToUnlock = _trait2.GetLevelsToUnlock(); }
        else { trait5LevelsToUnlock = _trait3.GetLevelsToUnlock(); }

        if (trait == _trait1 && _trait1.GetLevel() <= _minLevel)
        {
            _trait2.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait2.name} will unlock in {_trait1.GetLevelsToUnlock()} levels", "Red");
            _trait3.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait3.name} will unlock in {_trait1.GetLevelsToUnlock()} levels", "Red");
        }
        else if (trait == _trait2 && _trait2.GetLevel() <= _minLevel)
        {
            _trait4.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait4.name} will unlock in {_trait2.GetLevelsToUnlock()} levels", "Red");
            _trait5.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait5.name} will unlock in {trait5LevelsToUnlock} levels", "Red");
        }
        else if (trait == _trait3 && _trait3.GetLevel() <= _minLevel)
        {
            _trait5.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait5.name} will unlock in {trait5LevelsToUnlock} levels", "Red");
            _trait6.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait6.name} will unlock in {_trait3.GetLevelsToUnlock()} levels", "Red");
        }
    }

    private void UnlockTraits()
    {
        if (_trait1Unlocks && _trait1.GetLevel() == _minLevel)
        {
            _trait2.UnlockTrait();
            _trait3.UnlockTrait();
            _trait1Unlocks = false;
        }
        if (_trait2Unlocks && _trait2.GetLevel() == _minLevel)
        {
            _trait4.UnlockTrait();
            _trait5.UnlockTrait();
            _trait2Unlocks = false;
        }
        if (_trait3Unlocks && _trait3.GetLevel() == _minLevel)
        {
            _trait5.UnlockTrait();
            _trait6.UnlockTrait();
            _trait3Unlocks = false;
        }        
    }
}
