using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/Combat Spell")]
public class CombatSpell : Spell
{
    [Header("Gameplay")]
    public float damage;
    public float range;
    public float radius;
    public float effectAmount;
    public float effectDuration;
    public Element element;
}

