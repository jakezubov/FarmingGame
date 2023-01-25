public class MiningTraits : TraitHandler
{
    public Shovel _shovel;
    public Pickaxe _pickaxe;

    public override void PerformTraitChange(Trait trait)
    {
        SaveData.miningSP--;
        if (trait == _trait1) { SaveData.pickaxeEfficiencyLevel += 1; _pickaxe.SetPickaxeEfficiencyModifier(SaveData.pickaxeEfficiencyLevel); }
        else if (trait == _trait2) { SaveData.gemologistLevel += 1; }
        else if (trait == _trait3) { SaveData.prospectorLevel += 1; _pickaxe.SetProspectorModifier(SaveData.prospectorLevel); }
        else if (trait == _trait4) { SaveData.engineerLevel += 1; }
        else if (trait == _trait5) { SaveData.demolitionistLevel += 1; }
        else if (trait == _trait6) { SaveData.archaeologistLevel += 1; _shovel.SetArchaeologistModifier(SaveData.archaeologistLevel*2); }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.pickaxeEfficiencyLevel);
        _trait2.SetLevel(SaveData.gemologistLevel);
        _trait3.SetLevel(SaveData.prospectorLevel);
        _trait4.SetLevel(SaveData.engineerLevel);
        _trait5.SetLevel(SaveData.demolitionistLevel);
        _trait6.SetLevel(SaveData.archaeologistLevel);

        _pickaxe.SetPickaxeEfficiencyModifier(SaveData.pickaxeEfficiencyLevel);
        _pickaxe.SetProspectorModifier(SaveData.prospectorLevel);
        _shovel.SetArchaeologistModifier(SaveData.archaeologistLevel * 2);
    }
}
