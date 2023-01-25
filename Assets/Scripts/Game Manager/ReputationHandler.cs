using UnityEngine;

public class ReputationHandler : MonoBehaviour
{
    public Reputation _queen, _wizard, _blacksmith, _carpenter, _knights, _merchants, _farmers;

    void Start()
    {
        _queen.SetCurrentRep(SaveData.queenRep);
        _wizard.SetCurrentRep(SaveData.wizardRep);
        _blacksmith.SetCurrentRep(SaveData.blacksmithRep);
        _carpenter.SetCurrentRep(SaveData.carpenterRep);
        _knights.SetCurrentRep(SaveData.knightsRep);
        _merchants.SetCurrentRep(SaveData.merchantsRep);
        _farmers.SetCurrentRep(SaveData.farmersRep);
    }

    public void GainReputation(Rep rep, int amount)
    {
        switch(rep)
        {
            case Rep.queen: _queen.AddToCurrentRep(amount); SaveData.queenRep += amount; break;
            case Rep.wizard: _wizard.AddToCurrentRep(amount); SaveData.wizardRep += amount; break;
            case Rep.blacksmith: _blacksmith.AddToCurrentRep(amount); SaveData.blacksmithRep += amount; break;
            case Rep.carpenter: _carpenter.AddToCurrentRep(amount); SaveData.carpenterRep += amount; break;
            case Rep.knights: _knights.AddToCurrentRep(amount); SaveData.knightsRep += amount; break;
            case Rep.merchants: _merchants.AddToCurrentRep(amount); SaveData.merchantsRep += amount; break;
            case Rep.farmers: _farmers.AddToCurrentRep(amount); SaveData.farmersRep += amount; break;
        }
    }

    public void FreeReputation()
    {
        int gainedRep = 1000;
        GainReputation(Rep.queen, gainedRep);
        GainReputation(Rep.wizard, gainedRep);
        GainReputation(Rep.blacksmith, gainedRep);
        GainReputation(Rep.carpenter, gainedRep);
        GainReputation(Rep.knights, gainedRep);
        GainReputation(Rep.merchants, gainedRep);
        GainReputation(Rep.farmers, gainedRep);
    }
}

public enum Rep
{ 
    queen,
    wizard,
    blacksmith,
    carpenter,
    knights,
    merchants,
    farmers
}

