using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Spell")]
public class Spell : ScriptableObject
{
    [Header("Gameplay")]
    public float damage;
    public float range;
    public float radius;
    public float effectDuration;
    public Element element;   

    [Header("UI")] 
    public string description;    

    [Header("Both")]
    public float manaCost;
    public Sprite image;
    public Item[] components;
}

public enum Element
{
    NA,
    Arcane,
    Fire,
    Lightning,
    Ice
}

