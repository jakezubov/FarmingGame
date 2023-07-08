using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject _lootPrefab;
    public GameObject _parent;

    public void Drop(Vector3 position)
    {
        if (InventoryManager._instance.GetSelectedToolbarItem(false) != null)
        {
            // will drop an item on the ground if assigned key is pressed
            Item item = InventoryManager._instance.GetSelectedToolbarItem(true);
            position.y -= 0.5f;
            GameObject loot = Instantiate(_lootPrefab, position, Quaternion.identity);
            loot.transform.SetParent(_parent.transform);

            LootItem lootItem = loot.GetComponent<LootItem>();
            lootItem.Initialise(item);
            StartCoroutine(lootItem.ThrowItem(position));
        } 
    }
}
