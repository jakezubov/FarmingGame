using UnityEngine;

public class CraftingTraitPage : MonoBehaviour
{
    public Trait _extraCare, _recycler, _arrowForger, _hammerTime, _masterCraftsman, _hotterFurnace;

    private Skill _skill;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = PlayerManager._instance._crafting;
        _extraCare.SetLevelUpStatus(true);
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
        if (trait == _extraCare) { }
        else if (trait == _recycler) { }
        else if (trait == _arrowForger) { }
        else if (trait == _hammerTime) { }
        else if (trait == _masterCraftsman) { }
        else if (trait == _hotterFurnace) { }
    }

    private void UnlockTraits()
    {
        if (_extraCare.GetLevel() >= _minLevel)
        {
            _recycler.MakeButtonAvaliable();
            _recycler.SetLevelUpStatus(true);
            _arrowForger.MakeButtonAvaliable();
            _arrowForger.SetLevelUpStatus(true);
        }
        if (_recycler.GetLevel() >= _minLevel)
        {
            _hammerTime.MakeButtonAvaliable();
            _hammerTime.SetLevelUpStatus(true);
            _masterCraftsman.MakeButtonAvaliable();
            _masterCraftsman.SetLevelUpStatus(true);
        }
        if (_arrowForger.GetLevel() >= _minLevel)
        {
            _masterCraftsman.MakeButtonAvaliable();
            _masterCraftsman.SetLevelUpStatus(true);
            _hotterFurnace.MakeButtonAvaliable();
            _hotterFurnace.SetLevelUpStatus(true);
        }
    }
}
