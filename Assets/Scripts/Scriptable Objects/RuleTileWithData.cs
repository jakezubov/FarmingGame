using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Custom Rule Tile")]
public class RuleTileWithData : RuleTile
{
    [SerializeField] private Item droppedItem;

    public Item GetItem()
    {
        return droppedItem;
    }
}
