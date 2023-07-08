using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Custom Rule Tile")]
public class RuleTileWithData : RuleTile
{
    public RuleTileTags ruleTiletag;
    public Item[] droppedItems;
    public int health;

    public Item GetRandomItem()
    {
        if (droppedItems.Length > 1)
        {
            return droppedItems[Random.Range(0, droppedItems.Length)];
        }
        return droppedItems[0];
    }

    public Item GetMainItem()
    {
        return droppedItems[0];
    }

    public Item GetSecondaryItem()
    {
        if (droppedItems.Length > 1)
        {
            return droppedItems[Random.Range(1, droppedItems.Length)];
        }
        return droppedItems[0];
    }

    public void LowerHealth(int amount)
    {
        health -= amount;
    }
}

public enum RuleTileTags
{
    Foragable,
    Forestry,
    Mining,
    Farming,
    Fishing,
    Workbench
}


