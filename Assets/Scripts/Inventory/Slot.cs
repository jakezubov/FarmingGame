using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public InventoryItem _currentInventoryItem = null;

    public void OnDrop(PointerEventData eventData)
    {
        OnDropBase(eventData.pointerDrag.GetComponent<InventoryItem>());
    }

    public void OnDrop(InventoryItem item)
    {
        OnDropBase(item);
    }

    public virtual void OnDropBase(InventoryItem item)
    {
        Debug.Log("No assigned code");
    }

    public void SetCurrentItem(InventoryItem item)
    {
        _currentInventoryItem = item;
    }
}
