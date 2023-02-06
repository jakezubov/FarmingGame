public class CombatTraits : TraitHandler
{
    public PlayerController _player;
    public Stat _health;
    public Stat _stamina;

    private int _meleeAffinityModifier;
    private int _sturdyModifier;
    private int _rangedAffinityModifier;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { SaveData.moveSpeed += 0.1f; }
        else if (trait == _trait2) { _health.AddToMaxStatAmount(10); }
        else if (trait == _trait3) { _stamina.AddToMaxStatAmount(10); }
        else if (trait == _trait4) { _meleeAffinityModifier += 2; }
        else if (trait == _trait5) { _sturdyModifier += 1; }
        else if (trait == _trait6) { _rangedAffinityModifier += 2; }
    }

    public int GetMeleeAffinityExtraDamagePercentage()
    {
        return _meleeAffinityModifier * 2;
    }

    public int GetSturdyDamageReduction()
    {
        return _sturdyModifier;
    }

    public int GetRangedAffinityExtraDamagePercentage()
    {
        return _rangedAffinityModifier * 2;
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.hasteLevel);
        _trait2.SetLevel(SaveData.heartyLevel);
        _trait3.SetLevel(SaveData.enduranceLevel);
        _trait4.SetLevel(SaveData.meleeAffinityLevel);
        _trait5.SetLevel(SaveData.sturdyLevel);
        _trait6.SetLevel(SaveData.rangedAffinityLevel);

        _meleeAffinityModifier = SaveData.meleeAffinityLevel * 2;
    }

    public override void SaveTraitLevels()
    {
        SaveData.hasteLevel = _trait1.GetLevel();
        SaveData.heartyLevel = _trait2.GetLevel();
        SaveData.enduranceLevel = _trait3.GetLevel();
        SaveData.meleeAffinityLevel = _trait4.GetLevel();
        SaveData.sturdyLevel = _trait5.GetLevel();
        SaveData.rangedAffinityLevel = _trait6.GetLevel();
    }
}
