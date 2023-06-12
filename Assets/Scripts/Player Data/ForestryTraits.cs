public class ForestryTraits : TraitHandler
{
    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { SaveData.axeEfficiencyLevel++; }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { SaveData.fierceForagerLevel++; }
        else if (trait == _trait6) { SaveData.lumberjackLevel++; }
    }
}
