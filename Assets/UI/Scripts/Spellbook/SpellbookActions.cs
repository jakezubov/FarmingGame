using UnityEngine;

public class SpellbookActions : MonoBehaviour
{
    private static GameObject _spellbookElements, _spellbookAnimation;
    private static bool _bookAnimationPlaying = false;
    private static bool _spellbookActive = false;

    private void Awake()
    {
        _spellbookElements = GameObject.Find("Spellbook");
        _spellbookAnimation = GameObject.Find("Spellbook Animation");
    }

    public static bool IsBookAnimationPlaying()
    {
        return _bookAnimationPlaying;
    }

    public static void SetBookAnimationPlaying(bool b)
    {
        _bookAnimationPlaying = b;
    }

    public static bool IsSpellbookActive()
    {
        return _spellbookActive;
    }

    public static void SetSpellbookActive(bool b)
    {
        _spellbookActive = b;
        _spellbookElements.SetActive(b);
        _spellbookAnimation.SetActive(b);
    }
}
