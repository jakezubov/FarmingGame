public class ComponentSlot : Slot
{
    private InventoryItem _currentInventoryItem = null;

    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        if (transform.childCount == 0 && newInventoryItem.GetItem().GetItemType() == ItemType.SpellComponent)
        {
            newInventoryItem.SetParentAfterDrag(transform);
            _currentInventoryItem = newInventoryItem;
        }
        else if (transform.childCount == 1)
        {
            if (_currentInventoryItem.GetItem().IsStackable() && _currentInventoryItem.GetItem().name == newInventoryItem.GetItem().name &&
                (_currentInventoryItem.GetCount() + newInventoryItem.GetCount()) <= _currentInventoryItem.GetItem().GetMaxStackAmount())
            {
                _currentInventoryItem.AddToCount(newInventoryItem.GetCount());
                Destroy(newInventoryItem.gameObject);
            }
        }
    }

    public void SetCurrentItem(InventoryItem item)
    {
        _currentInventoryItem = item;
    }
}
