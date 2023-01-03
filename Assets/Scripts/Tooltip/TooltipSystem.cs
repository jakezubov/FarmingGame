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

    public static void Show(string description, string header = "", string colouredText = "", string colour = "", Sprite image = null)
    {
        _current._tooltip.SetText(description, header, colouredText, colour, image);
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
