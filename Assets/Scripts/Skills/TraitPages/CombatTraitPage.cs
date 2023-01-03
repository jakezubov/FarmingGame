using UnityEngine;

public class CombatTraitPage : MonoBehaviour
{
    public Trait _haste, _hearty, _endurance, _meleeAffinity, _sturdy, _rangedAffinity;   
    public PlayerController player;

    private Skill _skill;
    private readonly int _minLevel = 3;
    private readonly int _maxLevel = 10;

    private void Start()
    {
        _skill = PlayerManager._instance._combat;
        _haste.SetLevelUpStatus(true);
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
        if (trait == _haste) { player.AddToSpeed(0.1f); }
        else if (trait == _hearty) { PlayerManager._instance._health.AddToMaxStatAmount(10); }
        else if (trait == _endurance) { PlayerManager._instance._stamina.AddToMaxStatAmount(10); }
        else if (trait == _meleeAffinity) { }
        else if (trait == _sturdy) { }
        else if (trait == _rangedAffinity) { }
    }

    private void UnlockTraits()
    {
        if (_haste.GetLevel() >= _minLevel)
        {
            _hearty.MakeButtonAvaliable();
            _hearty.SetLevelUpStatus(true);
            _endurance.MakeButtonAvaliable();
            _endurance.SetLevelUpStatus(true);
        }
        if (_hearty.GetLevel() >= _minLevel)
        {
            _meleeAffinity.MakeButtonAvaliable();
            _meleeAffinity.SetLevelUpStatus(true);
            _sturdy.MakeButtonAvaliable();
            _sturdy.SetLevelUpStatus(true);
        }
        if (_endurance.GetLevel() >= _minLevel)
        {
            _sturdy.MakeButtonAvaliable();
            _sturdy.SetLevelUpStatus(true);
            _rangedAffinity.MakeButtonAvaliable();
            _rangedAffinity.SetLevelUpStatus(true);
        }
    }
}
