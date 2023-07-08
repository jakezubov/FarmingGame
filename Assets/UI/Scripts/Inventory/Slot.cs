using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    // this is the base class for all of the slot scripts
    // it just collects basic information and sets functions that need to be made

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
