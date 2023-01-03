using UnityEngine;

public class FarmingTraitPage : MonoBehaviour
{
    public Trait _seedSupplier, _charismatic, _fierceForager, _animalMagnetism, _timeIsMoney, _greenThumb;

    private Skill _skill;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = PlayerManager._instance._farming;
        _seedSupplier.SetLevelUpStatus(true);
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
        if (trait == _seedSupplier) { }
        else if (trait == _charismatic) { }
        else if (trait == _fierceForager) { }
        else if (trait == _animalMagnetism) { }
        else if (trait == _timeIsMoney) { }
        else if (trait == _greenThumb) { }
    }

    private void UnlockTraits()
    {
        if (_seedSupplier.GetLevel() >= _minLevel)
        {
            _charismatic.MakeButtonAvaliable();
            _charismatic.SetLevelUpStatus(true);
            _fierceForager.MakeButtonAvaliable();
            _fierceForager.SetLevelUpStatus(true);
        }
        if (_charismatic.GetLevel() >= _minLevel)
        {
            _animalMagnetism.MakeButtonAvaliable();
            _animalMagnetism.SetLevelUpStatus(true);
            _timeIsMoney.MakeButtonAvaliable();
            _timeIsMoney.SetLevelUpStatus(true);
        }
        if (_fierceForager.GetLevel() >= _minLevel)
        {
            _timeIsMoney.MakeButtonAvaliable();
            _timeIsMoney.SetLevelUpStatus(true);
            _greenThumb.MakeButtonAvaliable();
            _greenThumb.SetLevelUpStatus(true);
        }
    }
}
