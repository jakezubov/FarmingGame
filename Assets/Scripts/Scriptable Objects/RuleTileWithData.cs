using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Custom Rule Tile")]
public class RuleTileWithData : RuleTile
{
    [SerializeField] private Item[] droppedItems;

    public Item[] GetAllItems()
    {
        return droppedItems;
    }

    public Item GetRandomItem()
    {
        if (droppedItems.Length > 1)
        {
            return droppedItems[Random.Range(0, droppedItems.Length)];
        }
        return droppedItems[0];
    }
}
