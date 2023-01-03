using UnityEngine;

public class Reputation : MonoBehaviour
{
    public ChangeSlider _slider;
    private RepTier _repTier;
    private int _currentRep;
    private readonly int _totalRep = 10000;

    private int _acquaintanceRepAmount;
    private int _friendsRepAmount;
    private int _closeFriendsRepAmount;

    public Reputation()
    {
        _currentRep = 0;
        _repTier = RepTier.Stranger;
        _acquaintanceRepAmount = Mathf.RoundToInt(_totalRep * 1 / 4);
        _friendsRepAmount = Mathf.RoundToInt(_totalRep * 1 / 2);
        _closeFriendsRepAmount = Mathf.RoundToInt(_totalRep * 3 / 4);
    }

    public enum RepTier
    {
        Stranger,
        Acquaintance,
        Friends,
        CloseFriends
    };

    public void GainRep(int rep)
    {
        AddToCurrentRep(rep);
        if (_currentRep > _totalRep) { SetCurrentRep(_totalRep); }
        SetSlider(_currentRep);

        switch (_repTier)
        {
            case RepTier.Stranger: if (_currentRep >= _acquaintanceRepAmount) { _repTier = RepTier.Acquaintance; } break;
            case RepTier.Acquaintance: if (_currentRep >= _friendsRepAmount) { _repTier = RepTier.Friends; } break;
            case RepTier.Friends: if (_currentRep >= _closeFriendsRepAmount) { _repTier = RepTier.CloseFriends; } break;
            case RepTier.CloseFriends: break;
            default: _repTier = RepTier.Stranger; break;
        }
    }

    public int GetCurrentRep()
    {
        return _currentRep;
    }

    public void SetCurrentRep(int amount)
    {
        _currentRep = amount;
    }

    public void AddToCurrentRep(int amount)
    {
        _currentRep += amount;
    }

    public RepTier GetRepTier()
    {
        return _repTier;
    }

    public void SetRepTier(RepTier newTier)
    {
        _repTier = newTier;
    }

    public void SetSlider(int value)
    {
        _slider.SetValue(value);
    }
}
