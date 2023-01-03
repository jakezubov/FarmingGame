using UnityEngine;

public class MagicTraitPage : TraitPages
{
    /* Trait 1 = Swiftness
     * Trait 2 = Pack Mule
     * Trait 3 = Mana Overload
     * Trait 4 = Spell Affinity
     * Trait 5 = Willpower
     * Trait 6 = Ritual Affinity
     */

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { }
        else if (trait == _trait2) { }
        else if (trait == _trait3) { PlayerManager._instance._mana.AddToMaxStatAmount(10); }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }
}
