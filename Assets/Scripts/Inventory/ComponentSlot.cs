using UnityEngine;
using UnityEngine.EventSystems;

public class ComponentSlot : MonoBehaviour, IDropHandler
{
    private InventoryItem _currentInventoryItem = null;

    public void OnDrop(PointerEventData eventData)
    {
        OnDropBase(eventData.pointerDrag.GetComponent<InventoryItem>());               
    }

    public void NavOnDrop(InventoryItem item)
    {
        OnDropBase(item);
    }

    private void OnDropBase(InventoryItem newInventoryItem)
    {
        if (transform.childCount == 0 && newInventoryItem.GetItemType() == ItemType.SpellComponent)
        {
            newInventoryItem._parentAfterDrag = transform;
            _currentInventoryItem = newInventoryItem;
        }
        else if (transform.childCount == 1)
        {
            if (_currentInventoryItem.IsStackable() && _currentInventoryItem._item.name == newInventoryItem._item.name &&
                (_currentInventoryItem.GetCount() + newInventoryItem.GetCount()) <= _currentInventoryItem.GetMaxStackAmount())
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
