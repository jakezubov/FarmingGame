using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    public GameObject _inventoryObject, _skillsObject, _questsObject, _codexObject, _spellsObject, _optionsObject;
    public GameObject _inventoryFirstButton, _skillsFirstButton, _questsFirstButton, _codexFirstButton, _spellsFirstButton, _optionsFirstButton;
    public GameObject _toolbarObject, _generalElementsObject, _tabsObject;
    public Sprite _firstTab, _secondTab, _thirdTab, _fourthTab, _fifthTab, _sixthTab;
    public Color _selectedColour, _notSelectedColour;
    public Animator _animator;

    private bool _spellbookActive = false;
    private Sprite _currentTab;
    private Sections _sections = (Sections)1;
    private GameObject _currentSection, _currentButton;
    private readonly int _maxSectionNumber = Enum.GetNames(typeof(Sections)).Length;

    public enum Sections
    {
        inventory = 1,
        skills = 2,
        quests = 3,
        codex = 4,
        spells = 5,
        options = 6
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
        StartCoroutine(IOpenBook());    
    }

    private IEnumerator IOpenBook()
    {
        _animator.SetBool("Open", true);
        yield return new WaitForSeconds(3.5f);
        _animator.SetBool("Open", false);

        _generalElementsObject.SetActive(true);
        _tabsObject.SetActive(true);
        UpdateSpellBookSection();
        _spellbookActive = true;
    }

    public void CloseSpellbook()
    {
        // closes spellbook when the player presses the assigned key/s
        HideAllSections();
        _generalElementsObject.SetActive(false);
        _tabsObject.SetActive(false);

        StartCoroutine(ICloseBook()); 
    }   

    private IEnumerator ICloseBook()
    {
        _animator.SetBool("Close", true);
        yield return new WaitForSeconds(3.5f);
        _animator.SetBool("Close", false);

        _toolbarObject.SetActive(true);
        InventoryManager._instance.ChangeToolbarSelectedSlot(0);

        gameObject.SetActive(false);
        _spellbookActive = false;
    }

    public void CloseSpellbookFast()
    {
        HideAllSections();
        _generalElementsObject.SetActive(false);
        _tabsObject.SetActive(false);

        _toolbarObject.SetActive(true);
        InventoryManager._instance.ChangeToolbarSelectedSlot(0);

        gameObject.SetActive(false);
        _spellbookActive = false;
    }

    public void ChangeSectionLeft()
    {
        // changes current spellbook section to the left (or up if looking at bookmark tabs) 
        _sections--;
        if ((int)_sections == 0) { _sections = (Sections)_maxSectionNumber; }
        HideAllSections();
        StartCoroutine(ISectionLeft());
    }

    private IEnumerator ISectionLeft()
    {
        _generalElementsObject.SetActive(false);
        _animator.SetBool("PageLeft", true);

        yield return new WaitForSeconds(0.5f);

        _animator.SetBool("PageLeft", false);
        _generalElementsObject.SetActive(true);
        UpdateSpellBookSection();
    }

    public void ChangeSectionRight()
    {
        // changes current spellbook section to the right (or down if looking at bookmark tabs) 
        _sections++;
        if ((int)_sections == _maxSectionNumber + 1) { _sections = (Sections)1; }
        HideAllSections();
        StartCoroutine(ISectionRight());
    }

    private IEnumerator ISectionRight()
    {
        _generalElementsObject.SetActive(false);
        _animator.SetBool("PageRight", true);

        yield return new WaitForSeconds(0.5f);

        _animator.SetBool("PageRight", false);
        _generalElementsObject.SetActive(true);
        UpdateSpellBookSection();
    }

    private void UpdateSpellBookSection()
    {
        // sets spellbook to default state then selects new section to open
        FindCurrentObjects();
        HideAllSections();
        UnhideSection(_currentSection, _currentButton);
        _tabsObject.GetComponent<Image>().sprite = _currentTab;
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
        _questsObject.SetActive(false);
        _codexObject.SetActive(false);
        _spellsObject.SetActive(false);
        _optionsObject.SetActive(false);

        Deselect(_inventoryFirstButton.GetComponent<Image>());
        Deselect(_skillsFirstButton.GetComponent<Image>());
        Deselect(_questsFirstButton.GetComponent<Image>());
        Deselect(_codexFirstButton.GetComponent<Image>());
        Deselect(_spellsFirstButton.GetComponent<Image>());
        Deselect(_optionsFirstButton.GetComponent<Image>());
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
            case Sections.inventory: 
                _currentSection = _inventoryObject; 
                _currentButton = _inventoryFirstButton; 
                _currentTab = _firstTab; break;
            case Sections.skills: 
                _currentSection = _skillsObject; 
                _currentButton = _skillsFirstButton;
                _currentTab = _secondTab; break;
            case Sections.quests: 
                _currentSection = _questsObject; 
                _currentButton = _questsFirstButton;
                _currentTab = _thirdTab; break;
            case Sections.codex: 
                _currentSection = _codexObject; 
                _currentButton = _codexFirstButton;
                _currentTab = _fourthTab; break;
            case Sections.spells: 
                _currentSection = _spellsObject; 
                _currentButton = _spellsFirstButton;
                _currentTab = _fifthTab; break;
            case Sections.options: 
                _currentSection = _optionsObject; 
                _currentButton = _optionsFirstButton;
                _currentTab = _sixthTab; break;
            default: 
                _currentSection = _inventoryObject; 
                _currentButton = _inventoryFirstButton;
                _currentTab = _firstTab; break;
        }
    }

    
    public void ChangeSelectionFromTabs()
    {
        Sections currentSection = (Sections)_sections;

        // used by the bookmark tabs (when clicked) to change the current section
        if (_inventoryObject.activeSelf) { _sections = Sections.inventory; }   
        else if (_skillsObject.activeSelf) { _sections = Sections.skills; }
        else if (_questsObject.activeSelf) { _sections = Sections.quests; }
        else if (_codexObject.activeSelf) { _sections = Sections.codex; }
        else if (_spellsObject.activeSelf) { _sections = Sections.spells; }
        else if (_optionsObject.activeSelf) { _sections = Sections.options; }

        if ((int)currentSection > (int)_sections)
        {
            HideAllSections();
            StartCoroutine(ISectionLeft());
        }
        else if ((int)currentSection < (int)_sections)
        {
            HideAllSections();
            StartCoroutine(ISectionRight());
        }
    }

    public void SetToolbarActive(bool b)
    {
        // used to show or hide toolbar based on which section is open
        _toolbarObject.SetActive(b);
    }
}
