using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    #region Properties

    // objects that connect to each page of the book
    public GameObject _inventoryObject, _skillsObject, _questsObject, _codexObject, _spellsObject, _optionsObject;

    // objects that connect to the first button that is highlighted when opening section - is the tab icon on the right side
    public GameObject _inventoryFirstButton, _skillsFirstButton, _questsFirstButton, _codexFirstButton, _spellsFirstButton, _optionsFirstButton;

    // other objects - names corelate to objects in UI Canvas hierarchy
    public GameObject _toolbarObject, _generalElementsObject;

    // for toolbar - colours depending on if selected or not
    public Color _selectedColour, _notSelectedColour;

    // connects to the spellbooks animator
    public Animator _animator;

    // private variables for working within the class
    private bool _spellbookActive = false, _isBookAnimationPlaying = false;
    private Sections _sections = (Sections)1;
    private GameObject _currentSection, _currentButton;
    private readonly int _maxSectionNumber = Enum.GetNames(typeof(Sections)).Length;
    private float openCloseAnimLength = 1.5f;

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

    public bool CheckSpellbookActive()
    {
        return _spellbookActive;
    }

    public IEnumerator OpenSpellbook()
    {
        // opens spellbook when the player presses the assigned key
        gameObject.SetActive(true);
        _isBookAnimationPlaying = true;
        _animator.transform.GetComponent<FadeImage>().ChangeToFaded();
        yield return StartCoroutine(_animator.transform.GetComponent<FadeImage>().FadeIn());

        _animator.SetLayerWeight(_animator.GetLayerIndex("Tabs"), 0);
        _animator.SetBool("Open", true);
        yield return new WaitForSeconds(openCloseAnimLength);
        
        _animator.SetBool("Open", false);
        UpdateSpellBookSection();
        _generalElementsObject.SetActive(true);
        yield return StartCoroutine(FadeAllObjects(true));

        _isBookAnimationPlaying = false;
        _spellbookActive = true;
    }

    public IEnumerator CloseSpellbook()
    {
        // closes spellbook when the player presses the assigned key/s
        _isBookAnimationPlaying = true;
        yield return StartCoroutine(FadeAllObjects(false));
        
        _generalElementsObject.SetActive(false);
        HideAllSections();       

        _animator.SetLayerWeight(_animator.GetLayerIndex("Tabs"), 0);
        _animator.SetBool("Close", true);
        yield return new WaitForSeconds(openCloseAnimLength);

        _animator.SetBool("Close", false);
        yield return StartCoroutine(_animator.transform.GetComponent<FadeImage>().FadeOut());
      
        _isBookAnimationPlaying = false;
        _toolbarObject.SetActive(true);
        InventoryManager._instance.ChangeToolbarSelectedSlot(0);
        gameObject.SetActive(false);
        _spellbookActive = false;
    }

    public IEnumerator CloseSpellbookFast()
    {
        HideAllSections();
        _generalElementsObject.SetActive(true);

        _toolbarObject.SetActive(true);
        InventoryManager._instance.ChangeToolbarSelectedSlot(0);

        gameObject.SetActive(false);
        _spellbookActive = false;
        _animator.SetBool("Close", true);
        yield return new WaitForSeconds(openCloseAnimLength);

        _animator.SetBool("Close", false);  
    }

    public IEnumerator ChangeSectionLeft(bool decreaseSection)
    {
        if (decreaseSection)
        {
            _sections--;
            if ((int)_sections == 0) { _sections = (Sections)_maxSectionNumber; }
        } 

        _isBookAnimationPlaying = true;
        yield return StartCoroutine(FadeAllObjects(false));
        HideAllSections();

        _animator.SetLayerWeight(_animator.GetLayerIndex("Tabs"), 0); 
        _animator.SetBool("PageLeft", true);
        yield return new WaitForSeconds(0.65f);

        _animator.SetBool("PageLeft", false);
        UpdateSpellBookSection();
        yield return StartCoroutine(FadeAllObjects(true));

        _isBookAnimationPlaying = false; 
    }

    public IEnumerator ChangeSectionRight(bool increaseSection)
    {
        // changes current spellbook section to the right (or down if looking at bookmark tabs) 
        if (increaseSection)
        {
            _sections++;
            if ((int)_sections == _maxSectionNumber + 1) { _sections = (Sections)1; }
        }   

        _isBookAnimationPlaying = true;
        yield return StartCoroutine(FadeAllObjects(false));
        HideAllSections();

        _animator.SetLayerWeight(_animator.GetLayerIndex("Tabs"), 0);
        _animator.SetBool("PageRight", true);
        yield return new WaitForSeconds(0.65f);

        _animator.SetBool("PageRight", false);
        UpdateSpellBookSection();
        yield return StartCoroutine(FadeAllObjects(true));

        _isBookAnimationPlaying = false;
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

        _animator.SetBool("FirstTab", false);
        _animator.SetBool("SecondTab", false);
        _animator.SetBool("ThirdTab", false);
        _animator.SetBool("FourthTab", false);
        _animator.SetBool("FifthTab", false);
        _animator.SetBool("SixthTab", false);
    }

    public void ChangeSelectionFromTabs()
    {
        Sections beforeChangeSection = (Sections)_sections;

        // used by the bookmark tabs (when clicked) to change the current section
        if (_inventoryObject.activeSelf) { _sections = Sections.inventory; }
        else if (_skillsObject.activeSelf) { _sections = Sections.skills; }
        else if (_questsObject.activeSelf) { _sections = Sections.quests; }
        else if (_codexObject.activeSelf) { _sections = Sections.codex; }
        else if (_spellsObject.activeSelf) { _sections = Sections.spells; }
        else if (_optionsObject.activeSelf) { _sections = Sections.options; }

        if ((int)_sections < (int)beforeChangeSection)
        {
            StartCoroutine(ChangeSectionLeft(false));
        }
        else if ((int)_sections > (int)beforeChangeSection)
        {
            StartCoroutine(ChangeSectionRight(false));
        }
    }

    public void SetToolbarActive(bool b)
    {
        // used to show or hide toolbar based on which section is open
        _toolbarObject.SetActive(b);
    }

    public bool GetAnimationState()
    {
        return _isBookAnimationPlaying;
    }

    #endregion

    #region Private Methods

    private void UpdateSpellBookSection()
    {
        // sets spellbook to default state then selects new section to open
        FindCurrentObjects();
        UnhideSection(_currentSection, _currentButton);
        TooltipSystem.MoveMouse();

        if (_currentSection == _inventoryObject)
        {
            _toolbarObject.SetActive(true);
        }
        else { _toolbarObject.SetActive(false); }
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
        _animator.SetLayerWeight(_animator.GetLayerIndex("Tabs"), 1);

        // checks which section is active to assign _currentSection and _currentButton (move cursor here) to relevant values
        switch (_sections)
        {
            case Sections.inventory: 
                _currentSection = _inventoryObject; 
                _currentButton = _inventoryFirstButton;
                _animator.SetBool("FirstTab", true);  
                break;
            case Sections.skills: 
                _currentSection = _skillsObject; 
                _currentButton = _skillsFirstButton;
                _animator.SetBool("SecondTab", true);
                break;
            case Sections.quests: 
                _currentSection = _questsObject; 
                _currentButton = _questsFirstButton;
                _animator.SetBool("ThirdTab", true);
                break;
            case Sections.codex: 
                _currentSection = _codexObject; 
                _currentButton = _codexFirstButton;
                _animator.SetBool("FourthTab", true);
                break;
            case Sections.spells: 
                _currentSection = _spellsObject; 
                _currentButton = _spellsFirstButton;
                _animator.SetBool("FifthTab", true);
                break;
            case Sections.options: 
                _currentSection = _optionsObject; 
                _currentButton = _optionsFirstButton;
                _animator.SetBool("SixthTab", true);
                break;
            default: 
                _currentSection = _inventoryObject; 
                _currentButton = _inventoryFirstButton;
                _animator.SetBool("FirstTab", true);
                break;
        }
    }

    private IEnumerator FadeAllObjects(bool fadeIn)
    {
        FadeImage[] images = GetComponentsInChildren<FadeImage>();
        foreach (FadeImage image in images)
        {
            if (image.name != "Book Animation")
            {
                if (fadeIn)
                {
                    image.ChangeToFaded();
                    StartCoroutine(image.FadeIn());                
                }
                else
                {
                    StartCoroutine(image.FadeOut());
                }
            }  
        }

        FadeText[] texts = GetComponentsInChildren<FadeText>();
        foreach (FadeText text in texts)
        {
            if (fadeIn)
            {
                text.ChangeToFaded();
                StartCoroutine(text.FadeIn());
            }
            else
            {
                StartCoroutine(text.FadeOut());
            }
        }

        yield return new WaitForSeconds(0.1f);
    }


    #endregion
}
