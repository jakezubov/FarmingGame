using UnityEngine;
using UnityEngine.EventSystems;

public class TraitHandler : MonoBehaviour
{  
    public Trait _trait1, _trait2, _trait3, _trait4, _trait5, _trait6;
    public Skill _skill;

    private readonly float _efficiencyModifier = 0.2f;
    private readonly int _tier1unlock = 5, _tier2unlock = 15, _maxLevel = 10;  
    private bool _isTier1Unlocked = false, _isTier2Unlocked = false;

    void Start()
    {
        _trait1.UnlockTrait();
        ChangeLockedText();
        UnlockTraits();
    }

    public virtual void PerformTraitChange(Trait trait)
    {
        // used for inheritance
        Debug.Log("No assigned code");
    }

    public void TraitLevelUp()
    {
        // used when clicked with mouse or keyboard/controller to level up a trait
        Trait trait = EventSystem.current.currentSelectedGameObject.GetComponentInParent<Trait>();
        int currentSkillPoints = _skill.GetSkillPoints();

        if (trait.CheckLevelUpStatus() && currentSkillPoints > 0 && trait.GetLevel() < _maxLevel)
        {
            // increase a trait level and lower the avaliable skill points
            trait.IncreaseLevel();
            currentSkillPoints--;
            _skill.SetSkillPoints(currentSkillPoints);
            PerformTraitChange(trait);
        }
    }  

    public void ChangeLockedText()
    {
        // controls the locked text for each trait based on what the current skill level is
        if (_skill.GetLevel() < _tier1unlock)
        {
            _trait2.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait2.name} will unlock in {_tier1unlock - _skill.GetLevel()} {_skill.name} levels", "Red");
            _trait3.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait3.name} will unlock in {_tier1unlock - _skill.GetLevel()} {_skill.name} levels", "Red");
        }
        if (_skill.GetLevel() < _tier2unlock)
        {
            _trait4.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait4.name} will unlock in {_tier2unlock - _skill.GetLevel()} {_skill.name} levels", "Red");
            _trait5.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait5.name} will unlock in {_tier2unlock - _skill.GetLevel()} {_skill.name} levels", "Red");
            _trait6.GetComponent<TooltipTrigger>().SetSubHeading($"{_trait6.name} will unlock in {_tier2unlock - _skill.GetLevel()} {_skill.name} levels", "Red");
        }
    }

    public void UnlockTraits()
    {
        // unlock trait tiers when a specific skill level is met
        if (!_isTier1Unlocked && _skill.GetLevel() >= _tier1unlock)
        {
            _trait2.UnlockTrait();
            _trait3.UnlockTrait();
            _isTier1Unlocked = true;
        }
        if (!_isTier2Unlocked && _skill.GetLevel() >= _tier2unlock)
        {
            _trait4.UnlockTrait();
            _trait5.UnlockTrait();
            _trait6.UnlockTrait();
            _isTier1Unlocked = true;
        }    
    }

    public bool RollForExtras(int range)
    {
        int randChance = Random.Range(1, range + 1);
        if (randChance == 1)
        {
            return true;
        }
        else { return false; }
    }

    public float GetEfficiencyModifier()
    {
        return _efficiencyModifier;
    }
}
