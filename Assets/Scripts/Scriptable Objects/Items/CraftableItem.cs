using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Craftable Item")]
public class CraftableItem : ScriptableObject
{
    public Item craftedItem;
    public Item workbench;
    public Item[] requiredItems;
}
