using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public GameObject _itemPrefab;
    public CraftableItem[] _craftableItems;
    public Item[] _generalItems;

    void Start()
    {
        foreach (CraftableItem item in _craftableItems)
        {
            AddToCraftableList(item);
        }

        foreach (Item item in _generalItems)
        {
            AddToItemList(item);
        }
    }

    public void AddToCraftableList(CraftableItem item)
    {
        GameObject newCraftable = Instantiate(_itemPrefab, transform);
        newCraftable.GetComponent<Image>().sprite = item.craftedItem.inventoryImage;
        newCraftable.GetComponent<TooltipTrigger>().SetHeader(item.name);
        newCraftable.GetComponent<TooltipTrigger>().SetDescription(item.craftedItem.description);
    }

    public void AddToItemList(Item item)
    {
        GameObject newItem = Instantiate(_itemPrefab, transform);
        newItem.GetComponent<Image>().sprite = item.inventoryImage;
        newItem.GetComponent<TooltipTrigger>().SetHeader(item.name);
        newItem.GetComponent<TooltipTrigger>().SetDescription(item.description);
    }
}
