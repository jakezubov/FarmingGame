public class ComponentSlot : Slot
{
    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        if (newInventoryItem.GetItem().type == Type.SpellComponent)
        {
            if (transform.childCount == 0)
            {
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem = newInventoryItem;
            }
            else if (transform.childCount == 1)
            {
                if (_currentInventoryItem.GetItem().maxStack > 1 && _currentInventoryItem.GetItem().name == newInventoryItem.GetItem().name &&
                   (_currentInventoryItem.GetCount() + newInventoryItem.GetCount()) <= _currentInventoryItem.GetItem().maxStack)
                {
                    _currentInventoryItem.AddToCount(newInventoryItem.GetCount());
                    Destroy(newInventoryItem.gameObject);
                }
                else if (newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>())
                {
                    newInventoryItem.SetParentAfterDrag(transform);
                    _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                    newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>().SetCurrentItem(_currentInventoryItem);
                    _currentInventoryItem = newInventoryItem;
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
}
