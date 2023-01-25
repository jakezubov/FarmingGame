public class ForestryTraits : TraitHandler
{
    public Axe _axe;
    public Forage _forage;

    public override void PerformTraitChange(Trait trait)
    {
        SaveData.forestrySP--;
        if (trait == _trait1) { SaveData.axeEfficiencyLevel += 1; _axe.SetAxeEfficiencyModifier(SaveData.axeEfficiencyLevel); }
        else if (trait == _trait2) { SaveData.treeFertiliserLevel += 1; }
        else if (trait == _trait3) { SaveData.bountifulTreesLevel += 1; }
        else if (trait == _trait4) { SaveData.deforesterLevel += 1; }
        else if (trait == _trait5) { SaveData.fierceForagerLevel += 1; _forage.SetFierceForagerModifier(SaveData.fierceForagerLevel); }
        else if (trait == _trait6) { SaveData.lumberjackLevel += 1; _axe.SetLumberjackModifier(SaveData.lumberjackLevel); }
    }

    public override void LoadTraitLevels()
    {
        _trait1.SetLevel(SaveData.axeEfficiencyLevel);
        _trait2.SetLevel(SaveData.treeFertiliserLevel);
        _trait3.SetLevel(SaveData.bountifulTreesLevel);
        _trait4.SetLevel(SaveData.deforesterLevel);
        _trait5.SetLevel(SaveData.fierceForagerLevel);
        _trait6.SetLevel(SaveData.lumberjackLevel);

        _axe.SetAxeEfficiencyModifier(SaveData.axeEfficiencyLevel);
        _forage.SetFierceForagerModifier(SaveData.fierceForagerLevel);
        _axe.SetLumberjackModifier(SaveData.lumberjackLevel);
    }
}
