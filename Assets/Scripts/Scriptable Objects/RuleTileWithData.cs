using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Custom Rule Tile")]
public class RuleTileWithData : RuleTile
{
    [SerializeField] private Item _item;

    public Item GetItem()
    {
        return _item;
    }
}
