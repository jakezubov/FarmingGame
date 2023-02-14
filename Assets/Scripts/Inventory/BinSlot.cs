using UnityEngine;

public class BinSlot : Slot
{
    public override void OnDropBase(InventoryItem item)
    {
        // destroys any object placed in the bin
        if (item.GetParentBeforeDrag().GetComponent<ComponentSlot>())
        {
            InventoryManager._instance.SetComponentsFull(false);
        }
        Destroy(item.gameObject);        
    }
}
