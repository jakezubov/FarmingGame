using UnityEngine;
using UnityEngine.UI;

public class CraftableItemList : MonoBehaviour
{
    public Image _itemImage;
    public ChangeText _itemName;
    public GameObject _requiredComponents;
    public ChangeText _itemDescription;

    public GameObject _craftableItemPrefab;
    public GameObject _itemPrefab;
    public CraftableItem[] _currentItems;

    void Start()
    {
        foreach (CraftableItem item in _currentItems)
        {
            AddToCraftableList(item);
        }
        UpdateItemInformation(_currentItems[0]);
    }

    public void AddToCraftableList(CraftableItem item)
    {
        GameObject newCraftable = Instantiate(_craftableItemPrefab, transform);
        newCraftable.transform.GetChild(0).GetComponent<Image>().sprite = item.craftedItem.inventoryImage;
        newCraftable.GetComponentInChildren<SetupCraftableItem>().SetItem(item);
    }

    public void UpdateItemInformation(CraftableItem item)
    {
        _itemImage.sprite = item.craftedItem.inventoryImage;
        _itemName.SetText(item.name);
        _itemDescription.SetText(item.craftedItem.description);

        for (int i = 0; i < _requiredComponents.transform.childCount; i++)
        {
            Destroy(_requiredComponents.transform.GetChild(i).gameObject);
        }

        foreach (Item component in item.requiredItems)
        {
            GameObject newComponent = Instantiate(_itemPrefab, _requiredComponents.transform);
            newComponent.GetComponent<Image>().sprite = component.inventoryImage;
            newComponent.GetComponent<TooltipTrigger>().SetDescription(component.name);
        }
    }
}
