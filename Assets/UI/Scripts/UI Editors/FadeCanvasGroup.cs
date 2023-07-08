using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour
{
    public SpellbookAnimations _spellbook;

    public void FadeInFinish()
    {
        if (transform.name == "Spellbook Animation Canvas")
        {
            _spellbook.SpellbookAnimationFadeIn();
        }
        else if (transform.name == "Spellbook Elements Canvas")
        {
            _spellbook.SpellbookFadeIn();
        }
        else
        {
            Debug.Log("This isn't coded correctly!");
        }
    }

    public void FadeOutFinish()
    {
        if (transform.name == "Spellbook Animation Canvas")
        {
            _spellbook.SpellbookAnimationFadeOut();
        }
        else if (transform.name == "Spellbook Elements Canvas")
        {
            _spellbook.SpellbookFadeOut();
        }
        else
        {
            Debug.Log("This isn't coded correctly!");
        }
    }
}
