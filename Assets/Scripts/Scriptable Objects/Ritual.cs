using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Ritual")]
public class Ritual : ScriptableObject
{
    [Header("Gameplay")] 
    public float effectDuration;
    public Element element;

    [Header("UI")]
    public string description;

    [Header("Both")]
    public float manaCost;
    public Sprite image;
    public Item[] components;
}

