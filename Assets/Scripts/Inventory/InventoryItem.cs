using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{   
    public Image _image;
    public ChangeText _countText;

    private Item _item;
    private Transform _parentAfterDrag;
    private Transform _parentBeforeDrag;
    private int _count = 1;

    public void InitialiseItem(Item newItem)
    {
        _item = newItem;
        _image.sprite = newItem.inventoryImage;
        RefreshCount();

        // sets up tooltip
        TooltipTrigger tooltip = GetComponent<TooltipTrigger>();
        tooltip.SetHeader(_item.name);
        tooltip.SetDescription(_item.description);

        // shows max stack and item value if applicable in tooltip
        if (_item.maxStack > 1)
        {
            tooltip.SetExtraText($"Value: {_item.value}   Max Stack: {_item.maxStack}");
        }
        else { tooltip.SetExtraText($"Value: {_item.value}"); }

        // changes coloured text for specific items in tooltip
        if (newItem.type == Type.SpellComponent)
        {
            tooltip.SetSubHeading("Spell Component", "Magenta");
        }
        if (newItem.type == Type.Artefact)
        {
            tooltip.SetSubHeading("Artefact", "Cyan");
        }
    }

    public void RefreshCount()
    {
        // refreshes the count for how many items is in an inventory slot
        _countText.SetText(_count.ToString());
        bool textActive = _count > 1;
        _countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // begining dragging an item using the mouse
        _parentBeforeDrag = transform.parent;
        _parentAfterDrag = transform.parent;

        // moves item to transform.root so it appears infront of all other objects
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnBeginNav()
    {
        // begining dragging an item using the keyboard/controller
        _parentBeforeDrag = transform.parent;
        _parentAfterDrag = transform.parent;

        // moves item to transform.root so it appears infront of all other objects
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // while the item is being dragged with the keyboard make sure it follows the mouse
        Vector3 position = Mouse.current.position.ReadValue();
        position.z = Camera.main.nearClipPlane;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // when mouse click is released the item tries to drop into a new position
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector3.zero;
        _image.raycastTarget = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_parentAfterDrag.gameObject);

        // if the item was in a component slot then this updates the system to say the component pouch isn't full anymore
        if (_parentBeforeDrag.GetComponent<ComponentSlot>() && _parentAfterDrag.GetComponent<InventorySlot>())
        {
            InventoryManager._instance.SetComponentsFull(false);
        }
    }

    public void OnEndNav()
    {
        // when keyboard/controller inputed again the item tries to drop into a new position
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector3.zero;

        // if the item was in a component slot then this updates the system to say the component pouch isn't full anymore
        if (_parentBeforeDrag.GetComponent<ComponentSlot>() && _parentAfterDrag.GetComponent<InventorySlot>())
        {
            InventoryManager._instance.SetComponentsFull(false);
        }
    }

    public Item GetItem()
    {
        return _item;
    }

    public void SetParentAfterDrag(Transform newParent)
    {
        _parentAfterDrag = newParent;
    }

    public Transform GetParentBeforeDrag()
    {
        return _parentBeforeDrag;
    }

    public int GetCount()
    {
        return _count;
    }

    public void AddToCount(int num)
    {
        _count += num;
        RefreshCount();
    }
}
