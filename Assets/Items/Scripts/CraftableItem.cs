using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Craftable Item")]
public class CraftableItem : ScriptableObject
{
    public Item craftedItem;
    public Workbench workbench;
    public Item[] requiredItems;
}

public enum Workbench
{
    Kitchen,
    Anvil,
    WorkingStump,
    GrindingWheel,
    Smelter,
    RitualCircle
}