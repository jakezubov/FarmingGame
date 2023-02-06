using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableSpell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image _image;

    private Spell _spell;
    private Transform _parentBeforeDrag;

    public void InitialiseSpell(Spell spell)
    {
        _spell = spell;
        _image.sprite = spell.image;

        TooltipTrigger tooltip = GetComponent<TooltipTrigger>();
        tooltip.SetHeader(_spell.name);
        tooltip.SetDescription(_spell.description);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentBeforeDrag = _image.gameObject.transform.parent;
        _image.gameObject.transform.SetParent(transform.root);
        _image.gameObject.transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnBeginNav()
    {
        _parentBeforeDrag = _image.gameObject.transform.parent;
        _image.gameObject.transform.SetParent(transform.root);
        _image.gameObject.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = Mouse.current.position.ReadValue();
        position.z = Camera.main.nearClipPlane;
        _image.gameObject.transform.position = Camera.main.ScreenToWorldPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.gameObject.transform.SetParent(_parentBeforeDrag);
        _image.gameObject.transform.localPosition = Vector3.zero;
        _image.raycastTarget = true;
    }

    public void OnEndNav()
    {
        _image.gameObject.transform.SetParent(_parentBeforeDrag);
        _image.gameObject.transform.localPosition = Vector3.zero;
    }

    public Spell GetSpell()
    {
        return _spell;
    }
}
