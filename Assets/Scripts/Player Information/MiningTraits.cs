using UnityEngine;

public class MiningTraits : TraitHandler
{
    private float _pickAxeEfficiencyModifier;
    private int _gemologistModifier;
    private int _prospectorModifier;
    private int _archaeologistModifier;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _pickAxeEfficiencyModifier += _efficiencyModifier; }
        else if (trait == _trait2) { _gemologistModifier++; }
        else if (trait == _trait3) { _prospectorModifier++; }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { _archaeologistModifier += 2; }
    }

    public float GetPickaxeEfficiencyModifier()
    {
        return _pickAxeEfficiencyModifier;
    }

    public bool RollForGem()
    {
        int randChance = Random.Range(1, 30 - _gemologistModifier);
        if (randChance == 1)
        {
            return true;
        }
        else { return false; }
    }

    public bool RollForExtraOre()
    {
        int randChance = Random.Range(1, 15 - _prospectorModifier);
        if (randChance == 1)
        {
            return true;
        }
        else { return false; }
    }
    
    public bool RollForArtifact()
    {
        int randChance = Random.Range(1, 100 - _archaeologistModifier);
        if (randChance == 1)
        {
            return true;
        }
        else { return false; }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.pickaxeEfficiencyLevel);
        _trait2.SetLevel(SaveData.gemologistLevel);
        _trait3.SetLevel(SaveData.prospectorLevel);
        _trait4.SetLevel(SaveData.engineerLevel);
        _trait5.SetLevel(SaveData.demolitionistLevel);
        _trait6.SetLevel(SaveData.archaeologistLevel);

        _pickAxeEfficiencyModifier = SaveData.pickaxeEfficiencyLevel * _efficiencyModifier;
        _prospectorModifier = SaveData.prospectorLevel;
        _archaeologistModifier = SaveData.archaeologistLevel * 2;
    }

    public override void SaveTraitLevels()
    {
        SaveData.pickaxeEfficiencyLevel = _trait1.GetLevel();
        SaveData.gemologistLevel = _trait2.GetLevel();
        SaveData.prospectorLevel = _trait3.GetLevel();
        SaveData.engineerLevel = _trait4.GetLevel();
        SaveData.demolitionistLevel = _trait5.GetLevel();
        SaveData.archaeologistLevel = _trait6.GetLevel();
    }
}
