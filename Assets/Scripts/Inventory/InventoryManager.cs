using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;
    public GameObject _inventoryItemPrefab;

    public Item[] _startItems;
    public InventorySlot[] _inventorySlots;
    public ComponentSlot[] _componentSlots; 

    private int _selectedSlot = -1;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        ChangeToolbarSelectedSlot(0);
        foreach (var item in _startItems)
        {
            AddItem(item);
        }
    }

    public void ChangeToolbarSelectedSlot(int newSlot)
    {
        if (_selectedSlot >= 0) { _inventorySlots[_selectedSlot].Deselect(); }

        if (newSlot == -1) { _inventorySlots[_selectedSlot].Deselect(); }
        else
        {
            _inventorySlots[newSlot].Select();
            _selectedSlot = newSlot;
        }    
    }

    public bool AddItem(Item item)
    {
        // spawn item in component pouch if component 
        if (item._itemType == ItemType.SpellComponent)
        {
            for (int i = 0; i < _componentSlots.Length; i++)
            {
                ComponentSlot slot = _componentSlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.IsStackable() &&
                    itemInSlot._item == item && itemInSlot.GetCount() < itemInSlot.GetMaxStackAmount())
                {
                    itemInSlot.AddToCount(1);
                    return true;
                }
            }
            for (int i = 0; i < _componentSlots.Length; i++)
            {
                ComponentSlot slot = _componentSlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItemComp(item, slot);
                    return true;
                }
            }
        }
        else // else spawn item in inventory
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                InventorySlot slot = _inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.IsStackable() &&
                    itemInSlot._item == item && itemInSlot.GetCount() < itemInSlot.GetMaxStackAmount())
                {
                    itemInSlot.AddToCount(1);
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                InventorySlot slot = _inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItemInv(item, slot);
                    return true;
                }
            }
        }

        return false;
    }

    public void SpawnNewItemInv(Item item, InventorySlot slot)
    {
        GameObject newItemObj = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObj.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        slot.SetCurrentItem(inventoryItem);
    }

    public void SpawnNewItemComp(Item item, ComponentSlot slot)
    {
        GameObject newItemObj = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObj.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        slot.SetCurrentItem(inventoryItem);
    }

    public Item GetSelectedToolbarItem(bool use)
    {
        InventorySlot slot = _inventorySlots[_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot._item;
            if (use)
            {
                itemInSlot.AddToCount(-1);
                if (itemInSlot.GetCount() <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else { itemInSlot.RefreshCount(); }
            }
            return item;
        }
        return null;
    }
}
