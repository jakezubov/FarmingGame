using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/Utility Spell")]
public class UtilitySpell : Spell
{
    [Header("Gameplay")]
    public float range;
    public float radius;
    public float effectAmount;
    public float effectDuration;
}

