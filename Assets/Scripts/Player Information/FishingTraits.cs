using UnityEngine;

public class FishingTraits : TraitHandler
{
    private float _fishingEfficiencyModifier;
    private int _baitFinderModifier;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _fishingEfficiencyModifier += _efficiencyModifier; }
        else if (trait == _trait2) { _baitFinderModifier++; }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }

    public float GetFishingEfficiencyModifier()
    {
        return _fishingEfficiencyModifier;
    }

    public int GetBaitFinderModifier()
    {
        return _baitFinderModifier;
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.fishingEfficiencyLevel);
        _trait2.SetLevel(SaveData.baitFinderLevel);
        _trait3.SetLevel(SaveData.castMasterLevel);
        _trait4.SetLevel(SaveData.highQualityLuresLevel);
        _trait5.SetLevel(SaveData.breedingFrenzyLevel);
        _trait6.SetLevel(SaveData.strongLinesLevel);

        _fishingEfficiencyModifier = SaveData.fishingEfficiencyLevel * _efficiencyModifier;
        _baitFinderModifier = SaveData.baitFinderLevel;
    }

    public override void SaveTraitLevels()
    {
        SaveData.fishingEfficiencyLevel = _trait1.GetLevel();
        SaveData.baitFinderLevel = _trait2.GetLevel();
        SaveData.castMasterLevel = _trait3.GetLevel();
        SaveData.highQualityLuresLevel = _trait4.GetLevel();
        SaveData.breedingFrenzyLevel = _trait5.GetLevel();
        SaveData.strongLinesLevel = _trait6.GetLevel();
    }
}
