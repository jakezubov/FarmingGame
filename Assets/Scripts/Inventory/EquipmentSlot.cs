public class EquipmentSlot : Slot
{
    public override void OnDropBase(InventoryItem inventoryItem)
    {
        ItemType itemType = ItemType.Default;

        if (transform.childCount == 1)
        {
            if (CompareTag("Belt")) { itemType = ItemType.Belt; }
            if (CompareTag("Ring")) { itemType = ItemType.Ring; }
            if (CompareTag("ArcaneFocus")) { itemType = ItemType.ArcaneFocus; }
            if (CompareTag("Arrows")) { itemType = ItemType.Arrows; }
            if (CompareTag("Necklace")) { itemType = ItemType.Necklace; }

            if (inventoryItem.GetItem().GetItemType() == itemType)
            {
                inventoryItem.SetParentAfterDrag(transform);
            }
        }
    }
}
