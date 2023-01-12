using UnityEngine;

public class FishingTraitPage : TraitPages
{
    /* Trait 1 = Cast Master
     * Trait 2 = Bait Finder
     * Trait 3 = High Quality Lures
     * Trait 4 = No Time To Lose
     * Trait 5 = Breeding Frenzy
     * Trait 6 = Keen Sight
     */

    public Shovel _shovel;

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { }
        else if (trait == _trait2) { _shovel.AddToBaitFinderModifier(1); }
        else if (trait == _trait3) { }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }
}
