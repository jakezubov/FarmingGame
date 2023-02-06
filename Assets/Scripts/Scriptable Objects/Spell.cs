using UnityEngine;

public class Spell : ScriptableObject
{
    [Header("General")] 
    public string description;    
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

