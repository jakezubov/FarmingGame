using System;
using System.Collections;
using UnityEngine;

public class CombatTraitPage : TraitPages
{
    public PlayerController _player;

    /* Trait 1 = Haste
     * Trait 2 = Hearty
     * Trait 3 = Endurance
     * Trait 4 = Melee Affinity
     * Trait 5 = Sturdy
     * Trait 6 = Ranged Affinity
     */

    public override void PerformTraitChange(Trait trait)
    {
        if (trait == _trait1) { _player.AddToSpeed(0.1f); }
        else if (trait == _trait2) { PlayerManager._instance._health.AddToMaxStatAmount(10); }
        else if (trait == _trait3) { PlayerManager._instance._stamina.AddToMaxStatAmount(10); }
        else if (trait == _trait4) { }
        else if (trait == _trait5) { }
        else if (trait == _trait6) { }
    }    
}
