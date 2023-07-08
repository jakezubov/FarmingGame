using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    #region Properties

    // objects that connect to each page of the book
    public GameObject _inventoryObject, _skillsObject, _questsObject, _codexObject, _spellsObject, _optionsObject, _toolbarObject, _statsObject;

    // objects that connect to the first button that is highlighted when opening section - is the tab icon on the right side
    public GameObject _inventoryFirstButton, _skillsFirstButton, _questsFirstButton, _codexFirstButton, _spellsFirstButton, _optionsFirstButton;

    public SpellbookAnimations _spellbookAnimations;

    // for toolbar - colours depending on if selected or not
    public Color _selectedColour, _notSelectedColour;

    // private variables for working within the class
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

    #endregion

    #region Public Methods

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

    public void ChangeSectionLeft()
    {
        // changes current spellbook section to the left (or up if looking at bookmark tabs) 
        _sections--;
        if (_sections == 0) { _sections = (Sections)_maxSectionNumber; }
    }

    public void ChangeSectionRight()
    {
        // changes current spellbook section to the right (or down if looking at bookmark tabs) 
        _sections++;
        if ((int)_sections == _maxSectionNumber + 1) { _sections = (Sections)1; } 
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

    public void ChangeSelectionFromTabs(GameObject currentTab)
    {
        Sections beforeChangeSection = _sections;
        string tabName = currentTab.name[0..^4];

        // used by the bookmark tabs (when clicked) to change the current section
        if (tabName == _inventoryObject.name) { _sections = Sections.inventory; }
        else if (tabName == _skillsObject.name) { _sections = Sections.skills; }
        else if (tabName == _questsObject.name) { _sections = Sections.quests; }
        else if (tabName == _codexObject.name) { _sections = Sections.codex; }
        else if (tabName == _spellsObject.name) { _sections = Sections.spells; }
        else if (tabName == _optionsObject.name) { _sections = Sections.options; }

        if ((int)_sections < (int)beforeChangeSection)
        {
            _spellbookAnimations.SectionLeftStart(false);
        }
        else if ((int)_sections > (int)beforeChangeSection)
        {
            _spellbookAnimations.SectionRightStart(false);
        }
        else
        {
            Debug.Log("Same section as before!");
        }
    }

    public void UpdateSpellBookSection(Animator animator)
    {
        // sets spellbook to default state then selects new section to open
        FindCurrentObjects(animator);
        UnhideSection(_currentSection, _currentButton);
        TooltipSystem.MoveMouse();
        SetStatsActive(false);

        if (_currentSection == _inventoryObject)
        {
            SetToolbarActive(true);
        }
        else { SetToolbarActive(false); }
    }

    public void SetToolbarActive(bool b)
    {
        // used to show or hide toolbar based on which section is open
        _toolbarObject.SetActive(b);
    }

    public void SetStatsActive(bool b)
    {
        // used to show or hide stats
        _statsObject.SetActive(b);
    }

    #endregion

    #region Private Methods

    private void UnhideSection(GameObject section, GameObject firstButton)
    {
        // used to make a new spellbook section show on the screen
        section.SetActive(true);
        Select(firstButton.GetComponent<Image>());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private void FindCurrentObjects(Animator animator)
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Tabs"), 1);

        // checks which section is active to assign _currentSection and _currentButton (move cursor here) to relevant values
        switch (_sections)
        {
            case Sections.inventory: 
                _currentSection = _inventoryObject; 
                _currentButton = _inventoryFirstButton;
                animator.SetBool("FirstTab", true);  
                break;
            case Sections.skills: 
                _currentSection = _skillsObject; 
                _currentButton = _skillsFirstButton;
                animator.SetBool("SecondTab", true);
                break;
            case Sections.quests: 
                _currentSection = _questsObject; 
                _currentButton = _questsFirstButton;
                animator.SetBool("ThirdTab", true);
                break;
            case Sections.codex: 
                _currentSection = _codexObject; 
                _currentButton = _codexFirstButton;
                animator.SetBool("FourthTab", true);
                break;
            case Sections.spells: 
                _currentSection = _spellsObject; 
                _currentButton = _spellsFirstButton;
                animator.SetBool("FifthTab", true);
                break;
            case Sections.options: 
                _currentSection = _optionsObject; 
                _currentButton = _optionsFirstButton;
                animator.SetBool("SixthTab", true);
                break;
            default: 
                _currentSection = _inventoryObject; 
                _currentButton = _inventoryFirstButton;
                animator.SetBool("FirstTab", true);
                break;
        }
    }
    #endregion
}
