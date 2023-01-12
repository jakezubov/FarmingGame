using UnityEngine;

public class MiningTraitPage : TraitPages
{
    /* Trait 1 = Pickaxe Efficiency
     * Trait 2 = Map Maker
     * Trait 3 = Ore Miner
     * Trait 4 = Engineer
     * Trait 5 = Demolitionist
     * Trait 6 = Archaeologist
     */

    public Shovel _shovel;
    public Pickaxe _pickaxe;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _pickaxe.AddToPickaxeEfficiencyModifier(1); }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { _pickaxe.AddToOreMinerModifier(1); }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { _shovel.AddToArchaeologistModifier(2); }
    }
}
