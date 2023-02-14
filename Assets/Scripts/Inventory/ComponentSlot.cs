public class ComponentSlot : Slot
{
    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        // for dropping something onto a component slot, first checks if item is a spell component
        if (newInventoryItem.GetItem().type == Type.SpellComponent)
        {
            if (transform.childCount == 0) // dropping on slot with nothing in it
            {
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem = newInventoryItem;
            }
            else if (transform.childCount == 1) // dropping on slot that already has item
            {
                // checks if the slot the dragged item has come from is compatible to swap with the item in the current slot
                if (_currentInventoryItem.GetItem().maxStack > 1 && _currentInventoryItem.GetItem().name == newInventoryItem.GetItem().name &&
                   (_currentInventoryItem.GetCount() + newInventoryItem.GetCount()) <= _currentInventoryItem.GetItem().maxStack)
                {
                    _currentInventoryItem.AddToCount(newInventoryItem.GetCount());
                    Destroy(newInventoryItem.gameObject);
                }
                else if (newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>())
                {
                    // for spell components
                    newInventoryItem.SetParentAfterDrag(transform);
                    _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                    newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>().SetCurrentItem(_currentInventoryItem);
                    _currentInventoryItem = newInventoryItem;
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
