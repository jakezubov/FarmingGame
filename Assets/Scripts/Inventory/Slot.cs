using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
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
}
