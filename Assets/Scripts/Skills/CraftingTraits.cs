public class CraftingTraits : TraitHandler
{
    public override void PerformTraitChange(Trait trait)
    {
        SaveData.craftingSP--;
        if (trait == _trait1) { SaveData.extraCareLevel += 1; }
        else if (trait == _trait2) { SaveData.recyclerLevel += 1; }
        else if (trait == _trait3) { SaveData.arrowForgerLevel += 1; }
        else if (trait == _trait4) { SaveData.hammerTimeLevel += 1; }
        else if (trait == _trait5) { SaveData.masterCraftsmanLevel += 1; }
        else if (trait == _trait6) { SaveData.hotterfurnaceLevel += 1; }
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

}
