using UnityEngine;
using UnityEngine.EventSystems;

public class BinSlot : MonoBehaviour, IDropHandler
{
    void Start()
    {
        GetComponent<TooltipTrigger>().SetColouredText("*Warning*", "Red");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Destroy(eventData.pointerDrag);
    }

    public void NavOnDrop(InventoryItem item)
    {
        Destroy(item.gameObject);
    }
}
