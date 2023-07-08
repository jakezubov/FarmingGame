using UnityEngine;

public class Reputation : MonoBehaviour
{
    public ChangeSlider _slider;
    
    private RepTier _repTier;
    private int _currentRep;
    private readonly int _totalRep = 10000;
    private readonly int _acquaintanceRepAmount, _friendsRepAmount, _closeFriendsRepAmount;

    public Reputation()
    {
        // sets the reputation tier levels
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
    }

    public void AddToCurrentRep(int amount)
    {
        _currentRep += amount;
        _slider.SetValue(_currentRep);

        if (_currentRep > _totalRep) 
        {
            _currentRep = _totalRep;
            _slider.SetValue(_totalRep);
        }    

        switch (_repTier)
        {
            case RepTier.Stranger: if (_currentRep >= _acquaintanceRepAmount) { _repTier = RepTier.Acquaintance; } break;
            case RepTier.Acquaintance: if (_currentRep >= _friendsRepAmount) { _repTier = RepTier.Friends; } break;
            case RepTier.Friends: if (_currentRep >= _closeFriendsRepAmount) { _repTier = RepTier.CloseFriends; } break;
            case RepTier.CloseFriends: break;
        }
    }

    
    public void LoadRep(int amount)
    {
        // used for when loading game
        _currentRep = amount;
        _slider.SetValue(amount);

        if (amount > _closeFriendsRepAmount) { _repTier = RepTier.CloseFriends; }
        else if (amount > _friendsRepAmount) { _repTier = RepTier.Friends; }
        else if (amount > _acquaintanceRepAmount) { _repTier = RepTier.Acquaintance; }
        else { _repTier = RepTier.Stranger; }
    }

    public RepTier GetRepTier()
    {
        return _repTier;
    }

    public int GetCurrentRep()
    {
        return _currentRep;
    }
}
