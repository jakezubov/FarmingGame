using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    private ItemType _itemType;

    public void OnDrop(PointerEventData eventData)
    {
        OnDropBase(eventData.pointerDrag.GetComponent<InventoryItem>());
    }

    public void NavOnDrop(InventoryItem item)
    {
        OnDropBase(item);
    }

    private void OnDropBase(InventoryItem inventoryItem)
    {
        if (transform.childCount == 1)
        {
            if (CompareTag("Belt")) { _itemType = ItemType.Belt; }
            if (CompareTag("Ring")) { _itemType = ItemType.Ring; }
            if (CompareTag("ArcaneFocus")) { _itemType = ItemType.ArcaneFocus; }

            if (inventoryItem.GetItemType() == _itemType)
            {
                inventoryItem._parentAfterDrag = transform;
            }
        }
    }
}
