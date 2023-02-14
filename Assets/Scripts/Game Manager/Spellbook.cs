using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    public GameObject _inventoryObject, _skillsObject, _mapObject, _questsObject, _spellsObject, _reputationObject, _toolbarObject;
    public GameObject _inventoryFirstButton, _skillsFirstButton, _mapFirstButton, _questsFirstButton, _spellsFirstButton, _reputationFirstButton;
    public Color _selectedColour, _notSelectedColour;

    private bool _spellbookActive = false;
    private Sections _sections = (Sections)1;
    private GameObject _currentSection, _currentButton;
    private readonly int _maxSectionNumber = Enum.GetNames(typeof(Sections)).Length;

    public enum Sections
    {
        inventory = 1,
        skills = 2,
        map = 3,
        quests = 4,
        spells = 5,
        reputation = 6
    };

    public void Select(Image image)
    {
        // selected colour for the bookmark tabs
        image.color = _selectedColour;
    }

    public void Deselect(Image image)
    {
        // deselected colour for the bookmark tabs
        image.color = _notSelectedColour;
    }

    public bool CheckSpellbookActive()
    {
        return _spellbookActive;
    }

    public void OpenSpellbook()
    {
        // opens spellbook when the player presses the assigned key
        gameObject.SetActive(true);
        UpdateSpellBookSection();
        _spellbookActive = true;
    }

    public void CloseSpellbook()
    {
        // closes spellbook when the player presses the assigned key/s
        HideAllSections();
        gameObject.SetActive(false);    
        _spellbookActive = false;
        _toolbarObject.SetActive(true);
        InventoryManager._instance.ChangeToolbarSelectedSlot(0);
    }
   
    public void ChangeSectionLeft()
    {
        // changes current spellbook section to the left (or up if looking at bookmark tabs) 
        _sections--;
        if ((int)_sections == 0) { _sections = (Sections)_maxSectionNumber; }
        UpdateSpellBookSection();
    }

    public void ChangeSectionRight()
    {
        // changes current spellbook section to the right (or down if looking at bookmark tabs) 
        _sections++;
        if ((int)_sections == _maxSectionNumber + 1) { _sections = (Sections)1; }
        UpdateSpellBookSection();      
    }

    private void UpdateSpellBookSection()
    {
        // sets spellbook to default state then selects new section to open
        FindCurrentObjects();
        HideAllSections();
        UnhideSection(_currentSection, _currentButton);   
        TooltipSystem.MoveMouse();

        if (_currentSection == _inventoryObject)
        {
            _toolbarObject.SetActive(true);
        }
        else { _toolbarObject.SetActive(false); }
    }

    public void HideAllSections()
    {
        // default state of spellbook before selecting a new section
        TooltipSystem.Hide();
        _inventoryObject.SetActive(false);
        _skillsObject.SetActive(false);
        _mapObject.SetActive(false);
        _questsObject.SetActive(false);
        _spellsObject.SetActive(false);
        _reputationObject.SetActive(false);
        Deselect(_inventoryFirstButton.GetComponent<Image>());
        Deselect(_skillsFirstButton.GetComponent<Image>());
        Deselect(_mapFirstButton.GetComponent<Image>());
        Deselect(_questsFirstButton.GetComponent<Image>());
        Deselect(_spellsFirstButton.GetComponent<Image>());
        Deselect(_reputationFirstButton.GetComponent<Image>());
    }

    private void UnhideSection(GameObject section, GameObject firstButton)
    {
        // used to make a new spellbook section show on the screen
        section.SetActive(true);
        Select(firstButton.GetComponent<Image>());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private void FindCurrentObjects()
    {
        // checks which section is active to assign _currentSection and _currentButton (move cursor here) to relevant values
        switch(_sections)
        {
            case Sections.inventory: _currentSection = _inventoryObject; _currentButton = _inventoryFirstButton; break;
            case Sections.skills: _currentSection = _skillsObject; _currentButton = _skillsFirstButton; break;
            case Sections.map: _currentSection = _mapObject; _currentButton = _mapFirstButton; break;
            case Sections.quests: _currentSection = _questsObject; _currentButton = _questsFirstButton; break;
            case Sections.spells: _currentSection = _spellsObject; _currentButton = _spellsFirstButton; break;
            case Sections.reputation: _currentSection = _reputationObject; _currentButton = _reputationFirstButton; break;
            default: _currentSection = _inventoryObject; _currentButton = _inventoryFirstButton; break;
        }
    }

    
    public void ChangeSelectionFromTabs()
    {
        // used by the bookmark tabs (when clicked) to change the current section
        if (_inventoryObject.activeSelf) { _sections = Sections.inventory; }   
        else if (_skillsObject.activeSelf) { _sections = Sections.skills; }
        else if (_mapObject.activeSelf) { _sections = Sections.map; }
        else if (_questsObject.activeSelf) { _sections = Sections.quests; }
        else if (_spellsObject.activeSelf) { _sections = Sections.spells; }
        else if (_reputationObject.activeSelf) { _sections = Sections.reputation; }

        UpdateSpellBookSection();
    }

    public void SetToolbarActive(bool b)
    {
        // used to show or hide toolbar based on which section is open
        _toolbarObject.SetActive(b);
    }
}
