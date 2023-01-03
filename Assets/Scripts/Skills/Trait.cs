using UnityEngine;

public class Trait : MonoBehaviour
{
    public TraitButton _button;
    public ChangeText _text, _traitLocked;

    private int _traitLevel;
    private bool _canLevelUp;

    public Trait()
    {
        _traitLevel = 0;       
    }

    public int GetLevel()
    {
        return _traitLevel;
    }

    public void IncreaseLevel()
    {
        _traitLevel++;
        _text.SetText(_traitLevel.ToString());
    }

    public void MakeButtonAvaliable()
    {
        _button.ButtonAvaliableTrue();
    }

    public bool CheckLevelUpStatus()
    {
        return _canLevelUp;
    }

    public void SetLevelUpStatus(bool b)
    {
        _canLevelUp = b;
    }
}
