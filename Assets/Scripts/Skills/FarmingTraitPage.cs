using UnityEngine;

public class FarmingTraitPage : TraitPages
{
    /* Trait 1 = Seed Supplier
     * Trait 2 = Charismatic
     * Trait 3 = Fierce Forager
     * Trait 4 = Animal Magnetism
     * Trait 5 = Time Is Money
     * Trait 6 = Green Thumb
     */

    public Forage _forage;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { _forage.AddToFierceForagerModifier(1); }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }
}
