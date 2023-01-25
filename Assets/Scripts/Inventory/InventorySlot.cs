using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot
{
    public Image _image;
    public Color _selectedColour, _notSelectedColour;

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
            if (_currentInventoryItem.GetItem().maxStack > 1 && _currentInventoryItem.GetItem().name == newInventoryItem.GetItem().name &&
                _currentInventoryItem.GetCount() + newInventoryItem.GetCount() <= _currentInventoryItem.GetItem().maxStack)
            {
                _currentInventoryItem.AddToCount(newInventoryItem.GetCount());
                Destroy(newInventoryItem.gameObject);
            }
            else if (newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>() && _currentInventoryItem.GetItem().type == Type.SpellComponent)
            {
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                newInventoryItem.GetParentBeforeDrag().GetComponent<ComponentSlot>().SetCurrentItem(_currentInventoryItem);
                _currentInventoryItem = newInventoryItem;
            }
            else if (newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>())
            {
                if (_currentInventoryItem.GetItem().subType == newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().GetSlotType())
                {
                    newInventoryItem.SetParentAfterDrag(transform);
                    _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                    newInventoryItem.GetParentBeforeDrag().GetComponent<EquipmentSlot>().SetCurrentItem(_currentInventoryItem);
                    _currentInventoryItem = newInventoryItem;
                }
            }
            else if (newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>())
            {
                newInventoryItem.SetParentAfterDrag(transform);
                _currentInventoryItem.transform.SetParent(newInventoryItem.GetParentBeforeDrag());

                newInventoryItem.GetParentBeforeDrag().GetComponent<InventorySlot>().SetCurrentItem(_currentInventoryItem);
                _currentInventoryItem = newInventoryItem;
            }
        }
    }
}
