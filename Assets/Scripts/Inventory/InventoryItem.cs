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
    private int _count = 1;

    public void InitialiseItem(Item newItem)
    {
        _item = newItem;
        _image.sprite = newItem.GetImage();
        RefreshCount();

        TooltipTrigger tooltip = GetComponent<TooltipTrigger>();
        tooltip.SetHeader(_item.name);
        tooltip.SetDescription(_item.GetDescription());

        if (newItem.GetItemType() == ItemType.SpellComponent)
        {
            tooltip.SetColouredText(GetDescriptionFromEnum(ItemType.SpellComponent), newItem.GetTextColour());
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
        if (transform.parent.childCount == 2) { transform.parent.GetChild(0).gameObject.SetActive(true); }

        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnBeginNav()
    {
        if (transform.parent.childCount == 2) { transform.parent.GetChild(0).gameObject.SetActive(true); }

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

        if (_parentAfterDrag.childCount == 2) { _parentAfterDrag.GetChild(0).gameObject.SetActive(false); }
    }

    public void OnEndNav()
    {
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector3.zero;

        if (_parentAfterDrag.childCount == 2) { _parentAfterDrag.GetChild(0).gameObject.SetActive(false); }
    }

    public Item GetItem()
    {
        return _item;
    }

    public void SetParentAfterDrag(Transform newParent)
    {
        _parentAfterDrag = newParent;
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
