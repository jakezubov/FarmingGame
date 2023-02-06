using UnityEngine;

public class ForestryTraits : TraitHandler
{
    private float _axeEfficiencyModifier;
    private int _fierceForagerModifier;
    private int _lumberjackModifier;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _axeEfficiencyModifier += _efficiencyModifier; }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { _fierceForagerModifier++; }
        else if (trait == _trait6) { _lumberjackModifier++; }
    }

    public float GetAxeEfficiencyModifier()
    {
        return _axeEfficiencyModifier;
    }

    public bool RollForExtraForagable()
    {
        int randChance = Random.Range(1, 20 - _fierceForagerModifier);
        if (randChance == 1 && _fierceForagerModifier > 0)
        {
            return true;
        }
        else { return false; }
    }

    public bool RollForExtraWood()
    {
        int randChance = Random.Range(1, 11 - _lumberjackModifier);
        if (randChance == 1 && _lumberjackModifier > 0)
        {
            return true;
        }
        else { return false; }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.axeEfficiencyLevel);
        _trait2.SetLevel(SaveData.treeFertiliserLevel);
        _trait3.SetLevel(SaveData.bountifulTreesLevel);
        _trait4.SetLevel(SaveData.deforesterLevel);
        _trait5.SetLevel(SaveData.fierceForagerLevel);
        _trait6.SetLevel(SaveData.lumberjackLevel);

        _axeEfficiencyModifier = SaveData.axeEfficiencyLevel * _efficiencyModifier;
        _fierceForagerModifier = SaveData.fierceForagerLevel;
        _lumberjackModifier = SaveData.lumberjackLevel;
    }

    public override void SaveTraitLevels()
    {
        SaveData.axeEfficiencyLevel = _trait1.GetLevel();
        SaveData.treeFertiliserLevel = _trait2.GetLevel();
        SaveData.bountifulTreesLevel = _trait3.GetLevel();
        SaveData.deforesterLevel = _trait4.GetLevel();
        SaveData.fierceForagerLevel = _trait5.GetLevel();
        SaveData.lumberjackLevel = _trait6.GetLevel();
    }
}
