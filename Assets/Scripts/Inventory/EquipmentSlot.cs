public class EquipmentSlot : Slot
{
    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        // for dropping something onto an equipment slot, first checks if item is equipment
        if (newInventoryItem.GetItem().type == Type.Equipment)
        {
            Equipment newEquipment = (Equipment)newInventoryItem.GetItem();

            if (newEquipment.equipmentType == GetSlotType())
            {
                if (transform.childCount == 0) // dropping on slot with nothing in it
                {
                    newInventoryItem.SetParentAfterDrag(transform);
                    _currentInventoryItem = newInventoryItem;
                }
                else if (transform.childCount == 1) // dropping on slot that already has item
                {
                    // checks if item can be stacked onto the current item in the slot els it checks if
                    // the slot the dragged item has come from is compatible to swap with the item in the current slot
                    if (newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>())
                    {
                        // for equipment
                        if (newEquipment.equipmentType == newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().GetSlotType())
                        {
                            newInventoryItem.SetParentAfterDrag(transform);
                            _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                            newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().SetCurrentItem(_currentInventoryItem);
                            _currentInventoryItem = newInventoryItem;
                        }
                    }
                    else if (newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>())
                    {
                        // for inventory slots
                        newInventoryItem.SetParentAfterDrag(transform);
                        _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                        newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>().SetCurrentItem(_currentInventoryItem);
                        _currentInventoryItem = newInventoryItem;
                    }
                }
            }
        } 
    }

    public EquipmentType GetSlotType()
    {
        // slots are tagged to determine what goes in them so this just checks what tag is on the slot
        if (CompareTag("Belt")) { return EquipmentType.Belt; }
        else if (CompareTag("Ring")) { return EquipmentType.Ring; }
        else if (CompareTag("ArcaneFocus")) { return EquipmentType.ArcaneFocus; }
        else if (CompareTag("Arrows")) { return EquipmentType.Arrows; }
        else if (CompareTag("Necklace")) { return EquipmentType.Necklace; }
        else return EquipmentType.NA;
    }
}
