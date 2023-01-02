using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem _current;
    public Tooltip _tooltip;  

    public void Awake()
    {
        _current = this;
    }

    public static void Show(string content, string header = "", string traitChanges = "", string traitLocked = "", Sprite repTierUnlock = null)
    {
        _current._tooltip.SetText(content, header, traitChanges, traitLocked, repTierUnlock);
        _current._tooltip.gameObject.SetActive(true);       
    }

    public static void Hide()
    {
        _current._tooltip.gameObject.SetActive(false);        
    }

    public static void MoveMouse()
    {
        Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(EventSystem.current.currentSelectedGameObject.transform.position));
    }
}
