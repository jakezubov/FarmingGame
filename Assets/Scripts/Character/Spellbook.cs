using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spellbook : MonoBehaviour
{
    public GameObject _inventoryObject, _skillsObject, _reputationObject;
    public GameObject _inventoryFirstButton, _skillsFirstButton, _reputationFirstButton;
    public GameObject _skillPages;

    private bool _spellbookActive = false;
    private Sections _currentSection = (Sections)1;
    private readonly int _maxSectionNumber = Enum.GetNames(typeof(Sections)).Length;

    enum Sections
    {
        inventory = 1,
        skills = 2,
        reputation = 3
    };

    public void OpenSpellbook()
    {
        gameObject.SetActive(true);
        ChangeSpellBookPage();
        _spellbookActive = true;
    }

    public void CloseSpellbook()
    {
        HideAllSections();
        gameObject.SetActive(false);
        _spellbookActive = false;
    }

    public bool CheckSpellbookActive()
    {
        return _spellbookActive;
    }

    public void ChangeSectionLeft()
    {
        _currentSection--;
        if ((int)_currentSection == 0) { _currentSection = (Sections)_maxSectionNumber; }
        ChangeSpellBookPage();
    }

    public void ChangeSectionRight()
    {
        _currentSection++;
        if ((int)_currentSection == _maxSectionNumber + 1) { _currentSection = (Sections)1; }
        ChangeSpellBookPage();
    }

    public void HideAllSections()
    {
        TooltipSystem.Hide();
        _inventoryObject.SetActive(false);
        _skillsObject.SetActive(false);
        _reputationObject.SetActive(false);
    }

    public void UnhideSection(GameObject section, GameObject firstButton)
    {
        section.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    private GameObject FindCurrentSection()
    {
        GameObject section;
        switch(_currentSection)
        {
            case Sections.inventory: section = _inventoryObject; break;
            case Sections.skills: section = _skillsObject; break;
            case Sections.reputation: section = _reputationObject; break;
            default: section = _inventoryObject; break;
        }
        return section;
    }

    public  GameObject FindFirstButton()
    {
        GameObject firstButton;
        switch (_currentSection)
        {
            case Sections.inventory: firstButton = _inventoryFirstButton; break;
            case Sections.skills: firstButton = _skillsFirstButton; break;
            case Sections.reputation: firstButton = _reputationFirstButton; break;
            default: firstButton = _inventoryObject; break;
        }
        return firstButton;
    }

    private void ChangeSpellBookPage()
    {
        HideAllSections();   
        UnhideSection(FindCurrentSection(), FindFirstButton());
        TooltipSystem.MoveMouse();
    }
}
