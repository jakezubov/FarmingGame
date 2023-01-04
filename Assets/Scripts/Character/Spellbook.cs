using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spellbook : MonoBehaviour
{
    public GameObject _inventoryObject, _skillsObject, _reputationObject;
    public GameObject _inventoryFirstButton, _skillsFirstButton, _reputationFirstButton;

    private bool _spellbookActive = false;
    private Sections _sections = (Sections)1;
    private GameObject _currentSection;
    private GameObject _currentButton;
    private readonly int _maxSectionNumber = Enum.GetNames(typeof(Sections)).Length;

    enum Sections
    {
        inventory = 1,
        skills = 2,
        reputation = 3
    };

    public bool CheckSpellbookActive()
    {
        return _spellbookActive;
    }

    public void OpenSpellbook()
    {
        gameObject.SetActive(true);
        UpdateSpellBookSection();
        _spellbookActive = true;
    }

    public void CloseSpellbook()
    {
        HideAllSections();
        gameObject.SetActive(false);    
        _spellbookActive = false;
    }
   
    public void ChangeSectionLeft()
    {
        _sections--;
        if ((int)_sections == 0) { _sections = (Sections)_maxSectionNumber; }
        UpdateSpellBookSection();
    }

    public void ChangeSectionRight()
    {
        _sections++;
        if ((int)_sections == _maxSectionNumber + 1) { _sections = (Sections)1; }
        UpdateSpellBookSection();      
    }

    private void UpdateSpellBookSection()
    {
        FindCurrentObjects();
        HideAllSections();
        UnhideSection(_currentSection, _currentButton);
        TooltipSystem.MoveMouse();
    }

    public void HideAllSections()
    {
        TooltipSystem.Hide();
        _inventoryObject.SetActive(false);
        _skillsObject.SetActive(false);
        _reputationObject.SetActive(false);
    }

    private void UnhideSection(GameObject section, GameObject firstButton)
    {
        section.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private void FindCurrentObjects()
    {
        switch(_sections)
        {
            case Sections.inventory: _currentSection = _inventoryObject; _currentButton = _inventoryFirstButton; break;
            case Sections.skills: _currentSection = _skillsObject; _currentButton = _skillsFirstButton; break;
            case Sections.reputation: _currentSection = _reputationObject; _currentButton = _reputationFirstButton; break;
            default: _currentSection = _inventoryObject; _currentButton = _inventoryFirstButton; break;
        }
    }

    public void ChangeSelectionFromTabs()
    {
        if (_inventoryObject.activeSelf) { _sections = Sections.inventory; }   
        else if (_skillsObject.activeSelf) { _sections = Sections.skills; }
        else if (_reputationObject.activeSelf) { _sections = Sections.reputation; }

        UpdateSpellBookSection();
    }
}
