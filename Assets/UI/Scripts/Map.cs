using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
    public GameObject _mapMenu, _mapMenuFirstButton;
    public Spellbook _spellbook;

    public void SwitchMapMenuState()
    {
        if (!_mapMenu.activeInHierarchy)
        {
            _spellbook._toolbarObject.SetActive(false);
            _mapMenu.SetActive(true);
            PlayerActions.SetMapOpen(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_mapMenuFirstButton);
            TooltipSystem.MoveMouse();
        }
        else
        {
            _spellbook._toolbarObject.SetActive(true);
            _mapMenu.SetActive(false);
            PlayerActions.SetMapOpen(false);

            TooltipSystem.Hide();
        }
    }
}
