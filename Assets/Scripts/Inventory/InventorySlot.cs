using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image _image;
    public Color _selectedColour, _notSelectedColour;
    private InventoryItem _currentInventoryItem = null;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        _image.color = _selectedColour;
    }

    public void Deselect()
    {
        _image.color = _notSelectedColour;
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropBase(eventData.pointerDrag.GetComponent<InventoryItem>());
    }

    public void NavOnDrop(InventoryItem item)
    {
        OnDropBase(item); 
    }

    private void OnDropBase(InventoryItem newInventoryItem)
    {
        if (transform.childCount == 0)
        {
            newInventoryItem._parentAfterDrag = transform;
            _currentInventoryItem = newInventoryItem;
        }
        else if (transform.childCount == 1)
        {
            if (_currentInventoryItem.IsStackable() && _currentInventoryItem._item.name == newInventoryItem._item.name &&
                (_currentInventoryItem.GetCount() + newInventoryItem.GetCount()) <= _currentInventoryItem.GetMaxStackAmount())
            {
                _currentInventoryItem.AddToCount(newInventoryItem.GetCount());
                Destroy(newInventoryItem.gameObject);
            }
        }
    }

    public void SetCurrentItem(InventoryItem item)
    {
        _currentInventoryItem = item;
    }
}
