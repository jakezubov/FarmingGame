using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager _inventoryManager;
    public Item[] _itemsToPickUp;

    public void PickUpItem(int id)
    {
        bool result = _inventoryManager.AddItem(_itemsToPickUp[id]);
        if (result == true)
        {
            Debug.Log("Item Added");
        }
        else Debug.Log("Inventory Full");
    }

    public void GetSelectedItem()
    {
        Item receivedItem = _inventoryManager.GetSelectedToolbarItem(false);
        if (receivedItem != null)
        {
            Debug.Log($"Received {receivedItem}");
        }
        else Debug.Log("No Item Received");
    }

    public void UseSelectedItem()
    {
        Item receivedItem = _inventoryManager.GetSelectedToolbarItem(true);
        if (receivedItem != null)
        {
            Debug.Log($"Used {receivedItem}");
        }
        else Debug.Log("No Item Used");
    }
}
