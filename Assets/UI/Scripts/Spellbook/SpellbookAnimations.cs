using UnityEngine;

public class SpellbookAnimations : MonoBehaviour
{
    // connects to the spellbooks animator
    public Spellbook _spellbook;
    public GameObject _generalElementsObject, _statsObject;
    public Animator _spellbookAnimationAnimator, _fadeSpellbookElementsAnimator, _fadeSpellbookAnimationsAnimator;

    private string _activeAnimation;

    # region Open Spellbook

    public void OpenSpellbookStart()
    {
        // opens spellbook when the player presses the assigned key/s
        SpellbookActions.SetSpellbookActive(true);
        SpellbookActions.SetBookAnimationPlaying(true);
        _activeAnimation = "Open";
        _fadeSpellbookAnimationsAnimator.SetBool("FadeIn", true);
    }

    public void SpellbookAnimationFadeIn()
    {
        // activates after key frame event from OpenSpellbookStart()
        _fadeSpellbookAnimationsAnimator.SetBool("FadeIn", false);
        _spellbookAnimationAnimator.SetLayerWeight(_spellbookAnimationAnimator.GetLayerIndex("Tabs"), 0);
        _spellbookAnimationAnimator.SetBool("Open", true);
    }

    public void OpenSpellbookFinish()
    {
        // activates after key frame event from SpellbookAnimationFadeIn()
        _spellbookAnimationAnimator.SetBool("Open", false);
        _spellbook.UpdateSpellBookSection(_spellbookAnimationAnimator);
        SetElementsActive(true);
        _fadeSpellbookElementsAnimator.SetBool("FadeIn", true);
    }

    #endregion

    # region Close Spellbook

    public void CloseSpellbookStart()
    {
        // closes spellbook when the player presses the assigned key/s
        SpellbookActions.SetBookAnimationPlaying(true);
        _activeAnimation = "Close";
        _fadeSpellbookElementsAnimator.SetBool("FadeOut", true);
    }

    public void CloseSpellbookFinish()
    {
        // activates after key frame event from SpellbookFadeOut()
        _spellbookAnimationAnimator.SetBool("Close", false);
        _fadeSpellbookAnimationsAnimator.SetBool("FadeOut", true);
    }

    public void SpellbookAnimationFadeOut()
    {
        // activates after key frame event from CloseSpellbookFinish()
        _fadeSpellbookAnimationsAnimator.SetBool("FadeOut", false);
        _spellbook.SetToolbarActive(true);
        _spellbook.SetStatsActive(true);
        SpellbookActions.SetBookAnimationPlaying(false);
        InventoryManager._instance.ChangeToolbarSelectedSlot(0);
        SpellbookActions.SetBookAnimationPlaying(false);
        SpellbookActions.SetSpellbookActive(false);
    }

    # endregion

    # region Page Turning

    public void SectionLeftStart(bool decreaseSection)
    {
        // changes current spellbook section to the left (or up if looking at bookmark tabs) 
        if (decreaseSection) { _spellbook.ChangeSectionLeft(); }
        SpellbookActions.SetBookAnimationPlaying(true);
        _activeAnimation = "PageLeft";
        _fadeSpellbookElementsAnimator.SetBool("FadeOut", true);
    }

    public void SectionLeftFinish()
    {
        // activates after key frame event in animation
        _spellbookAnimationAnimator.SetBool("PageLeft", false);
        _spellbook.UpdateSpellBookSection(_spellbookAnimationAnimator);
        _fadeSpellbookElementsAnimator.SetBool("FadeIn", true);
    }

    public void SectionRightStart(bool increaseSection)
    {
        // changes current spellbook section to the right (or down if looking at bookmark tabs) 
        if (increaseSection) { _spellbook.ChangeSectionRight(); }
        SpellbookActions.SetBookAnimationPlaying(true);
        _activeAnimation = "PageRight";
        _fadeSpellbookElementsAnimator.SetBool("FadeOut", true);
    }

    public void SectionRightFinish()
    {
        // activates after key frame event in animation
        _spellbookAnimationAnimator.SetBool("PageRight", false);
        _spellbook.UpdateSpellBookSection(_spellbookAnimationAnimator);
        _fadeSpellbookElementsAnimator.SetBool("FadeIn", true);
    }

    # endregion

    public void SpellbookFadeOut()
    {
        // activates after key frame event from CloseSpellbookStart() or SectionLeftStart() or SectionRightStart()
        _fadeSpellbookElementsAnimator.SetBool("FadeOut", false);
        ResetTabAnimations();
        _spellbook.HideAllSections();
        if (_activeAnimation == "Close") { SetElementsActive(false); }

        _spellbookAnimationAnimator.SetLayerWeight(_spellbookAnimationAnimator.GetLayerIndex("Tabs"), 0);
        _spellbookAnimationAnimator.SetBool(_activeAnimation, true);
    }

    public void SpellbookFadeIn()
    {
        // activates after key frame event from OpenSpellbookFinish() or SectionLeftFinish() or SectionRightFinish()
        _fadeSpellbookElementsAnimator.SetBool("FadeIn", false);
        SpellbookActions.SetBookAnimationPlaying(false);
    }

    private void ResetTabAnimations()
    {
        _spellbookAnimationAnimator.SetBool("FirstTab", false);
        _spellbookAnimationAnimator.SetBool("SecondTab", false);
        _spellbookAnimationAnimator.SetBool("ThirdTab", false);
        _spellbookAnimationAnimator.SetBool("FourthTab", false);
        _spellbookAnimationAnimator.SetBool("FifthTab", false);
        _spellbookAnimationAnimator.SetBool("SixthTab", false);
    }

    private void SetElementsActive(bool b)
    {
        // used to show or hide elements
        _generalElementsObject.SetActive(b);
    }
}
