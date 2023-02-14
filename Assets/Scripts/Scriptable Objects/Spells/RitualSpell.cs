using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spells/Ritual Spell")]
public class RitualSpell : Spell
{
    [Header("Gameplay")]
    public float effectAmount;
    public float effectDuration;
    public Element element;
}

