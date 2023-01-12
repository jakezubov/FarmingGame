using UnityEngine;

public class WoodcuttingTraitPage : TraitPages
{
    /* Trait 1 = Axe Efficiency
     * Trait 2 = Leaf Clearing
     * Trait 3 = Greater Chopper
     * Trait 4 = Tree Fertiliser
     * Trait 5 = Lumberjack
     * Trait 6 = Wood Farmer
     */

    public Axe _axe;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _axe.AddToAxeEfficiencyModifier(1); }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { _axe.AddToWoodFarmerModifier(1); }
    }
}
