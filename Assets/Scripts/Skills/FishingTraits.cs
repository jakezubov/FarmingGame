public class FishingTraits : TraitHandler
{
    public Shovel _shovel;

    public override void PerformTraitChange(Trait trait)
    {
        SaveData.fishingSP--;
        if (trait == _trait1) { SaveData.fishingEfficiencyLevel += 1; }
        else if (trait == _trait2) { SaveData.baitFinderLevel += 1; _shovel.SetBaitFinderModifier(SaveData.baitFinderLevel); }
        else if (trait == _trait3) { SaveData.castMasterLevel += 1; }
        else if (trait == _trait4) { SaveData.highQualityLuresLevel += 1; }
        else if (trait == _trait5) { SaveData.breedingFrenzyLevel += 1; }
        else if (trait == _trait6) { SaveData.strongLinesLevel += 1; }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.fishingEfficiencyLevel);
        _trait2.SetLevel(SaveData.baitFinderLevel);
        _trait3.SetLevel(SaveData.castMasterLevel);
        _trait4.SetLevel(SaveData.highQualityLuresLevel);
        _trait5.SetLevel(SaveData.breedingFrenzyLevel);
        _trait6.SetLevel(SaveData.strongLinesLevel);

        _shovel.SetBaitFinderModifier(SaveData.baitFinderLevel);
    }
}
