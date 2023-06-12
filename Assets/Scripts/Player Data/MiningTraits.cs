public class MiningTraits : TraitHandler
{
    public Item[] _artefacts;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { SaveData.pickaxeEfficiencyLevel++; }
        else if (trait == _trait2) { SaveData.gemologistLevel++; }
        else if (trait == _trait3) { SaveData.prospectorLevel++; }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { SaveData.archaeologistLevel += 2; }
    }

    public Item[] GetArtefactList()
    {
        return _artefacts;
    }
}
