public class FishingTraits : TraitHandler
{
    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { SaveData.fishingEfficiencyLevel++; }
        else if (trait == _trait2) { SaveData.baitFinderLevel++; }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }
}
