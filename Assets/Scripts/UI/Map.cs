using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
    public GameObject _mapMenu, _mapMenuFirstButton;
    public Spellbook _spellbook;

    public void OpenMapMenu()
    {
        _spellbook._toolbarObject.SetActive(false);
        _mapMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_mapMenuFirstButton);
        TooltipSystem.MoveMouse();
    }

    public void CloseMapMenu()
    {
        _spellbook._toolbarObject.SetActive(true);
        _mapMenu.SetActive(false);

        TooltipSystem.Hide();
    }
}
