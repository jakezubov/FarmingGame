public class CraftingTraits : TraitHandler
{
    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.extraCareLevel);
        _trait2.SetLevel(SaveData.recyclerLevel);
        _trait3.SetLevel(SaveData.arrowForgerLevel);
        _trait4.SetLevel(SaveData.hammerTimeLevel);
        _trait5.SetLevel(SaveData.masterCraftsmanLevel);
        _trait6.SetLevel(SaveData.hotterfurnaceLevel);
    }

    public override void SaveTraitLevels()
    {
        SaveData.extraCareLevel = _trait1.GetLevel();
        SaveData.recyclerLevel = _trait2.GetLevel();
        SaveData.arrowForgerLevel = _trait3.GetLevel();
        SaveData.hammerTimeLevel = _trait4.GetLevel();
        SaveData.masterCraftsmanLevel = _trait5.GetLevel();
        SaveData.hotterfurnaceLevel = _trait6.GetLevel();
    }
}
