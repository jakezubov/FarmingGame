using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Tool")]
public class Tool : Item
{
    [Header("Gameplay")]
    public ToolType toolType;
    public float damage;
    public int maxDurability;
    public int durability;
}

public enum ToolType
{
    NA,
    Axe,
    FishingRod,
    Hammer,
    Hoe,
    Pickaxe,
    Shovel,
    WateringCan
}