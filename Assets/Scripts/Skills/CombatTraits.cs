using UnityEngine;

public class CombatTraits : TraitHandler
{
    public PlayerController _player;
    public Stats _stats;

    public override void PerformTraitChange(Trait trait)
    {
        SaveData.combatSP--;
        if (trait == _trait1) { SaveData.hasteLevel += 1; SaveData.moveSpeed += 0.1f; }
        else if (trait == _trait2) { SaveData.heartyLevel += 1; _stats.AddToMaxStatAmount(Stat.health, 10); }
        else if (trait == _trait3) { SaveData.enduranceLevel += 1; _stats.AddToMaxStatAmount(Stat.stamina, 10); }
        else if (trait == _trait4) { SaveData.meleeAffinityLevel += 1; }
        else if (trait == _trait5) { SaveData.sturdyLevel += 1; }
        else if (trait == _trait6) { SaveData.rangedAffinityLevel += 1; }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.hasteLevel);
        _trait2.SetLevel(SaveData.heartyLevel);
        _trait3.SetLevel(SaveData.enduranceLevel);
        _trait4.SetLevel(SaveData.meleeAffinityLevel);
        _trait5.SetLevel(SaveData.sturdyLevel);
        _trait6.SetLevel(SaveData.rangedAffinityLevel);
    }
}
