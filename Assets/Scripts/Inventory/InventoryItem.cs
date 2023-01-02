using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image _image;
    public ChangeText _countText;

    [HideInInspector] public Item _item;   
    [HideInInspector] public Transform _parentAfterDrag;

    //private Vector3 _position;
    private int _count = 1;

    public void InitialiseItem(Item newitem)
    {
        _item = newitem;
        _image.sprite = newitem._image;
        RefreshCount();

        TooltipTrigger tooltip = GetComponent<TooltipTrigger>();
        tooltip._header = _item.name;
        tooltip._content = _item._description;
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

        if (_parentAfterDrag.childCount == 2) { _parentAfterDrag.GetChild(0).gameObject.SetActive(false); }        
    }

    public void OnEndNav()
    {
        transform.SetParent(_parentAfterDrag);
        transform.localPosition = Vector3.zero;

        if (_parentAfterDrag.childCount == 2) { _parentAfterDrag.GetChild(0).gameObject.SetActive(false); }
    }

    public ItemType GetItemType()
    {
        return _item._itemType;
    }

    public int GetMaxStackAmount()
    {
        return _item.GetMaxStack();
    }

    public void SetMaxStackAmount(int amount)
    {
        _item.SetMaxStack(amount);
    }

    public void AddToCount(int num)
    {
        _count += num;
        RefreshCount();
    }

    public int GetCount()
    {
        return _count;
    }

    public bool IsStackable()
    {
        return _item._stackable;
    }
}
