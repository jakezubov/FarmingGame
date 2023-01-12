public class EquipmentSlot : Slot
{
    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        if (newInventoryItem.GetItem().equipmentType == GetSlotType())
        {
            if (transform.childCount == 0)
            {
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem = newInventoryItem;
            }
            else if (transform.childCount == 1)
            {
                if (newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>())
                {
                    if (_currentInventoryItem.GetItem().equipmentType == newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().GetSlotType())
                    {
                        newInventoryItem.SetParentAfterDrag(transform);
                        _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                        newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().SetCurrentItem(_currentInventoryItem);
                        _currentInventoryItem = newInventoryItem;
                    }   
                }
                else if (newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>())
                {
                    newInventoryItem.SetParentAfterDrag(transform);
                    _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                    newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>().SetCurrentItem(_currentInventoryItem);
                    _currentInventoryItem = newInventoryItem;
                }
            }
        } 
    }

    public EquipmentType GetSlotType()
    {
        if (CompareTag("Belt")) { return EquipmentType.Belt; }
        else if (CompareTag("Ring")) { return EquipmentType.Ring; }
        else if (CompareTag("ArcaneFocus")) { return EquipmentType.ArcaneFocus; }
        else if (CompareTag("Arrows")) { return EquipmentType.Arrows; }
        else if (CompareTag("Necklace")) { return EquipmentType.Necklace; }
        else { return EquipmentType.NA; }
    }
}
