public class FarmingTraits : TraitHandler
{
    public override void PerformTraitChange(Trait trait)
    {
        SaveData.farmingSP--;
        if (trait == _trait1) { SaveData.farmingEfficiencyLevel += 1; }
        else if (trait == _trait2) { SaveData.seedSupplierLevel += 1; }
        else if (trait == _trait3) { SaveData.retainingSoilLevel += 1;  }
        else if (trait == _trait4) { SaveData.animalMagnetismLevel += 1; }
        else if (trait == _trait5) { SaveData.rancherLevel += 1; }
        else if (trait == _trait6) { SaveData.greenThumbLevel += 1; }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.farmingEfficiencyLevel);
        _trait2.SetLevel(SaveData.seedSupplierLevel);
        _trait3.SetLevel(SaveData.retainingSoilLevel);
        _trait4.SetLevel(SaveData.animalMagnetismLevel);
        _trait5.SetLevel(SaveData.rancherLevel);
        _trait6.SetLevel(SaveData.greenThumbLevel);
    }
}
