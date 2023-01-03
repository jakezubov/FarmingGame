using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public Item[] _itemsToPickUp;

    public void PickUpItem(int id)
    {
        InventoryManager._instance.AddItem(_itemsToPickUp[id]);
    }
}
