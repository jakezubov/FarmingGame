using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.ComponentModel;
using System.Reflection;
using System;

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
        _image.sprite = newItem.image;
        RefreshCount();

        TooltipTrigger tooltip = GetComponent<TooltipTrigger>();
        tooltip.SetHeader(_item.name);
        tooltip.SetDescription(_item.description);

        if (newItem.itemType == ItemType.SpellComponent)
        {
            tooltip.SetColouredText("Spell Component", "Magenta");
        }
        if (newItem.itemType == ItemType.Artefact)
        {
            tooltip.SetColouredText("Artefact", "Cyan");
        }
    }

    public static string GetDescriptionFromEnum(Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes =
          (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes == null && attributes.Length == 0 ? value.ToString() : attributes[0].Description;
    }

    public void RefreshCount()
    {
        _countText.SetText(_count.ToString());
        bool textActive = _count > 1;
        _countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentBeforeDrag = transform.parent;
        _parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnBeginNav()
    {
        _parentBeforeDrag = transform.parent;
        _parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = Mouse.current.position.ReadValue();
        position.z = Camera.main.nearClipPlane;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector3.zero;
        _image.raycastTarget = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_parentAfterDrag.gameObject);

        if (_parentBeforeDrag.GetComponent<ComponentSlot>() && _parentAfterDrag.GetComponent<InventorySlot>())
        {
            InventoryManager._instance.SetComponentsFull(false);
        }
    }

    public void OnEndNav()
    {
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector3.zero;

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
