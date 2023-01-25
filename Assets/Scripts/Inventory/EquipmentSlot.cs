public class EquipmentSlot : Slot
{
    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        if (newInventoryItem.GetItem().subType == GetSlotType())
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
                    if (_currentInventoryItem.GetItem().subType == newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().GetSlotType())
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

    public SubType GetSlotType()
    {
        if (CompareTag("Belt")) { return SubType.Belt; }
        else if (CompareTag("Ring")) { return SubType.Ring; }
        else if (CompareTag("ArcaneFocus")) { return SubType.ArcaneFocus; }
        else if (CompareTag("Arrows")) { return SubType.Arrows; }
        else if (CompareTag("Necklace")) { return SubType.Necklace; }
        else { return SubType.NA; }
    }
}
