public class EquipmentSlot : Slot
{
    public override void OnDropBase(InventoryItem inventoryItem)
    {
        EquipmentType equipmentType = EquipmentType.NA;

        if (transform.childCount == 1)
        {
            if (CompareTag("Belt")) { equipmentType = EquipmentType.Belt; }
            else if (CompareTag("Ring")) { equipmentType = EquipmentType.Ring; }
            else if (CompareTag("ArcaneFocus")) { equipmentType = EquipmentType.ArcaneFocus; }
            else if (CompareTag("Arrows")) { equipmentType = EquipmentType.Arrows; }
            else if (CompareTag("Necklace")) { equipmentType = EquipmentType.Necklace; }

            if (inventoryItem.GetItem().equipmentType == equipmentType)
            {
                inventoryItem.SetParentAfterDrag(transform);
            }
        }
    }
}
