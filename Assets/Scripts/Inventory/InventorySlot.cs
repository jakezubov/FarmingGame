using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot
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

    public override void OnDropBase(InventoryItem newInventoryItem)
    {
        if (transform.childCount == 0)
        {
            newInventoryItem.SetParentAfterDrag(transform);
            _currentInventoryItem = newInventoryItem;
        }
        else if (transform.childCount == 1)
        {
            if (_currentInventoryItem.GetItem().stackable && _currentInventoryItem.GetItem().name == newInventoryItem.GetItem().name &&
                (_currentInventoryItem.GetCount() + newInventoryItem.GetCount()) <= _currentInventoryItem.GetItem().maxStack)
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
