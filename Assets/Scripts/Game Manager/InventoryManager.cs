using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;
    public GameObject _inventoryItemPrefab;

    public Item[] _startItems;
    public InventorySlot[] _inventorySlots;
    public ComponentSlot[] _componentSlots; 

    private int _selectedSlot = -1;
    private bool _componentsFull = false;

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
        // if component check if item can fit in component pouch
        if (item.type == Type.SpellComponent)
        {
            foreach (ComponentSlot slot in _componentSlots)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (slot.GetComponent<Button>().interactable == true)
                {
                    if (itemInSlot != null && itemInSlot.GetItem().maxStack > 1 &&
                    itemInSlot.GetItem() == item && itemInSlot.GetCount() < itemInSlot.GetItem().maxStack)
                    {
                        itemInSlot.AddToCount(1);
                        return true;
                    }
                }          
            }
            if (!_componentsFull)
            {
                foreach (ComponentSlot slot in _componentSlots)
                {
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                    if (slot.GetComponent<Button>().interactable == true)
                    {
                        if (itemInSlot == null)
                        {
                            SpawnNewItemComp(item, slot);
                            return true;
                        }
                    }
                }
            }  
            _componentsFull = true;
        }   
        
        foreach (InventorySlot slot in _inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.GetItem().maxStack > 1 &&
                itemInSlot.GetItem() == item && itemInSlot.GetCount() < itemInSlot.GetItem().maxStack)
            {
                itemInSlot.AddToCount(1);
                return true;
            }
        }
        foreach (InventorySlot slot in _inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItemInv(item, slot);
                return true;
            }
        }      
        return false;
    }

    private void SpawnNewItemInv(Item item, InventorySlot slot)
    {
        GameObject newItemObj = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObj.GetComponent<InventoryItem>();

        inventoryItem.InitialiseItem(item);
        slot.SetCurrentItem(inventoryItem);
    }

    private void SpawnNewItemComp(Item item, ComponentSlot slot)
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
            Item item = itemInSlot.GetItem();
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

    public void SetComponentsFull(bool b)
    {
        _componentsFull = b;
    }
}
