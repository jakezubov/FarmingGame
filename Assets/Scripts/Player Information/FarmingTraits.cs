public class FarmingTraits : TraitHandler
{
    private float _farmingEfficiencyModifier;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _farmingEfficiencyModifier += _efficiencyModifier; }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }

    public float GetFarmingEfficiencyModifier()
    {
        return _farmingEfficiencyModifier;
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

    public override void SaveTraitLevels()
    {
        SaveData.farmingEfficiencyLevel = _trait1.GetLevel();
        SaveData.seedSupplierLevel = _trait2.GetLevel();
        SaveData.retainingSoilLevel = _trait3.GetLevel();
        SaveData.animalMagnetismLevel = _trait4.GetLevel();
        SaveData.rancherLevel = _trait5.GetLevel();
        SaveData.greenThumbLevel = _trait6.GetLevel();

        _farmingEfficiencyModifier = SaveData.farmingEfficiencyLevel * _efficiencyModifier;
    }
}
